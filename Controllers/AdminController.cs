using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.Converters;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Data.ResponseDto_s;
using Car_Rental_Backend_Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Rental_Backend_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly CarRentalContext _context;

        public AdminController(CarRentalContext context)
        {
            _context = context;
        }

   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminResponseDto>>> GetAdmins()
        {
            var admins = await _context.Admin.ToListAsync();
            var adminDtos = admins.Select(AdminConverters.AdminToAdminResponseDto).ToList();
            return Ok(adminDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminResponseDto>> GetAdminById(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
                return NotFound($"Admin with ID {id} not found.");

            return Ok(AdminConverters.AdminToAdminResponseDto(admin));
        }

    

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AdminRequestLoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Login data is required.");

            var admin = await _context.Admin
                .FirstOrDefaultAsync(a => (a.Email == loginDto.Email || a.Username == loginDto.Email)
                                          && a.Password == loginDto.Password);

            if (admin == null)
                return Unauthorized("Invalid credentials.");

            return Ok($"Welcome, {admin.Username}! Login successful.");
        }

    }
}
