using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.DTOs;
using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CancellationController : ControllerBase
{
    private readonly CarRentalContext _context;
    private readonly EmailService _emailService;

    public CancellationController(CarRentalContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<ActionResult<CancellationResponseDto>> CreateCancellation(CancellationRequestDto cancellationRequestDto)
    {
        if (cancellationRequestDto == null)
            return BadRequest("Cancellation data is required.");

        try
        {
            var booking = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BookingId == cancellationRequestDto.Booking_ID);

            if (booking == null)
                return NotFound("Booking not found.");

            var existingCancellation = await _context.Cancellations
                .FirstOrDefaultAsync(c => c.Booking_ID == cancellationRequestDto.Booking_ID);

            if (existingCancellation != null)
                return BadRequest("This booking has already been canceled.");

            // ✅ Create Cancellation Entity
            var cancellation = new Cancellation
            {
                Booking_ID = booking.BookingId,
                Cancellation_Date = DateTime.UtcNow,
                Reason = cancellationRequestDto.Reason
            };

            // ✅ Save Cancellation First
            _context.Cancellations.Add(cancellation);
            await _context.SaveChangesAsync();

            // ✅ Update Car Availability
            if (booking.Car != null)
            {
                booking.Car.Availability_Status = "Available";
                _context.Cars.Update(booking.Car);
            }

            // ✅ Remove Booking
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync(); // Save changes after deletion

            // ✅ Email Notification
            string emailSubject = "Booking Cancellation Confirmation - Car Rental";
            string emailBody = $@"
        <h2>Dear {booking.User.Username},</h2>
        <p>Your booking has been successfully canceled.</p>
        <h3>Cancellation Details:</h3>
        <ul>
            <li><strong>Booking ID:</strong> {booking.BookingId}</li>
            <li><strong>Car Model:</strong> {booking.Car.Model}</li>
            <li><strong>Pickup Date:</strong> {booking.PickupDate:yyyy-MM-dd}</li>
            <li><strong>Return Date:</strong> {booking.ReturnDate:yyyy-MM-dd}</li>
            <li><strong>Cancellation Date:</strong> {cancellation.Cancellation_Date:yyyy-MM-dd}</li>
            <li><strong>Reason:</strong> {cancellation.Reason}</li>
        </ul>
        <p>We hope to serve you again in the future!</p>
        <p>Best Regards, <br/>Car Rental Team</p>";

            await _emailService.SendEmailAsync(booking.User.Email, emailSubject, emailBody);

            // ✅ Return Response
            var cancellationResponseDto = new CancellationResponseDto
            {
                Cancellation_ID = cancellation.Cancellation_ID,
                Booking_ID = cancellation.Booking_ID,
                Cancellation_Date = cancellation.Cancellation_Date,
                Reason = cancellation.Reason
            };

            return CreatedAtAction(nameof(GetCancellationById), new { id = cancellationResponseDto.Cancellation_ID }, cancellationResponseDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CancellationResponseDto>> GetCancellationById(int id)
    {
        var cancellation = await _context.Cancellations.FindAsync(id);

        if (cancellation == null)
            return NotFound($"Cancellation with ID {id} not found.");

        return Ok(CancellationConverters.CancellationToCancellationResponseDto(cancellation));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CancellationResponseDto>>> GetAllCancellations()
    {
        var cancellations = await _context.Cancellations.ToListAsync();

        if (!cancellations.Any())
            return NotFound("No cancellations found.");

        return Ok(cancellations.Select(CancellationConverters.CancellationToCancellationResponseDto));
    }
}
