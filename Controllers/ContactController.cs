using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Car_Rental_Backend_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly CarRentalContext _context; // Correct the variable name here to _context
        private readonly EmailService _emailService;

        // Correct constructor name and assign the correct context variable
        public ContactController(CarRentalContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactRequestDto contactRequestDto)
        {
            if (contactRequestDto == null)
            {
                return BadRequest("Contact information is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map the ContactRequestDto to a Contact entity
            var contact = new Contact
            {
                Full_Name = contactRequestDto.Full_Name,
                Email = contactRequestDto.Email,
                PhoneNumber = contactRequestDto.PhoneNumber,
                Message = contactRequestDto.Message
            };

            // Save the contact using the service/repository (assuming SaveContactAsync is a method in the context)
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Send Confirmation Email
            string subject = "Thank you for contacting us!";
            string message = $"<h3>Hello {contact.Full_Name},</h3><p>Thank you for reaching out to us. We will contact you as soon as possible.</p>";
            await _emailService.SendEmailAsync(contact.Email, subject, message);

            // Return the created contact (can be extended to return a status code or message as well)
            return CreatedAtAction(nameof(GetContactById), new { id = contact.ContactId }, contact);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return Ok(contacts);
        }
    }
}
