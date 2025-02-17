using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Car_Rental_Backend_Application.Controllers
{
    [Route("api/admin/bookings")]
    [ApiController]
    public class AdminBookingController : ControllerBase
    {
        private readonly CarRentalContext _context;

        public AdminBookingController(CarRentalContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] AdminBookingRequestDto bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest("Booking details are required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = new Booking
            {
                User_ID = bookingDto.User_ID,
                UserName = bookingDto.UserName,
                Car_ID = bookingDto.Car_ID,
                CarDetails = bookingDto.CarDetails,
                BookingDate = bookingDto.BookingDate,
                PickupDate = bookingDto.PickupDate,
                ReturnDate = bookingDto.ReturnDate,
                TotalPrice = bookingDto.TotalPrice
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
    }
}
