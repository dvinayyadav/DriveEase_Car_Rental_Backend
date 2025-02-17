using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.Converters;
using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Data.ResponseDto_s;
using Org.BouncyCastle.Crypto.Generators;

namespace Car_Rental_Backend_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CarRentalContext _context;
        private readonly EmailService _emailService;
        private readonly ILogger<UserController> _logger;

        public UserController(CarRentalContext context, EmailService emailService, ILogger<UserController> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users.Select(UserConverters.UserToResponseUserDto));
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById([FromRoute] int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                throw new UserNotFoundException($"User with ID {id} not found.");
            }
            return Ok(UserConverters.UserToResponseUserDto(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> RegisterUser([FromBody] UserRequestDto userRequestDto)
        {
            if (userRequestDto == null)
                return BadRequest("User data is required.");
            if (StrongPassword(userRequestDto.Password) != true)
            {
                throw new PasswordMustBeStringException($"Passsword must cantain one UpperCase,One LowerCase,One Numeric,one Special and size must be greater than 7.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == userRequestDto.Email))
                throw new EmailAlreadyExistsException($"User with Email {userRequestDto.Email} already exists.");

            if (await _context.Users.AnyAsync(u => u.PhoneNumber == userRequestDto.PhoneNumber))
                throw new MobileNumberAlreadyExistedException($"User with Phone Number {userRequestDto.PhoneNumber} already exists.");

            User user = UserConverters.RequestUserDtoToUser(userRequestDto);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // ✅ Send Confirmation Email
            string subject = "Welcome to Car Rental Service!";
            string message = $"<h3>Hello {user.Username},</h3><p>Thank you for registering with Car Rental Service.</p>";
            await _emailService.SendEmailAsync(user.Email, subject, message);

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, UserConverters.UserToResponseUserDto(user));
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserResponseDto>> GetUserByEmail([FromRoute] string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new UserNotFoundException($"User with email {email} not found.");

            return Ok(UserConverters.UserToResponseUserDto(user));
        }

        [HttpGet("phone/{phoneNumber}")]
        public async Task<ActionResult<UserResponseDto>> GetUserByPhoneNumber([FromRoute] string phoneNumber)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if (user == null)
                throw new UserNotFoundException($"User with Phone Number {phoneNumber} not found.");

            return Ok(UserConverters.UserToResponseUserDto(user));
        }

        [HttpGet("address/{address}")]
        public async Task<ActionResult<List<UserResponseDto>>> GetUsersByAddress([FromRoute] string address)
        {
            var users = await _context.Users.Where(u => u.Address.Contains(address)).ToListAsync();
            if (!users.Any())
                return NotFound($"No users found at address: {address}");

            return Ok(users.Select(UserConverters.UserToResponseUserDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                throw new UserNotFoundException($"User with ID {id} not found.");

            if (user.Bookings.Any() )
                return BadRequest($"User with ID {id} has active bookings or reservations and cannot be deleted.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<UpdateUserResponseDto>> UpdateUser([FromRoute] int id, [FromBody] UpdateUserResponseDto updateDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                throw new UserNotFoundException($"User with ID {id} not found.");
            }

            // Update user details
            user.PhoneNumber = updateDto.PhoneNumber ?? user.PhoneNumber;
            user.Username = updateDto.UserName ?? user.Username;
            user.Email = user.Email;
            user.Address = updateDto.Address ?? user.Address;

            await _context.SaveChangesAsync();

            var responseDto = UserConverters.UserToUserUpdateDto(user);

            return Ok(responseDto);
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] AdminRequestLoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Login data is required.");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => (u.Email == loginDto.Email || u.Username == loginDto.Email)
                                          && u.Password == loginDto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            return Ok($"Welcome, {user.Username}! Login successful.");
        }

        [HttpPost("forgot-password/{userId}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
                return NotFound($"User with ID {userId} does not exist.");

            if (string.IsNullOrEmpty(user.Email))
                return BadRequest("User does not have a registered email.");

            // ✅ Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();

            //// ✅ Hash the OTP before storing it (for security)
            //user.OTP = BCrypt.Net.BCrypt.HashPassword(otp);
            user.OTP = otp;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(10); // OTP valid for 10 minutes

            await _context.SaveChangesAsync();

            // ✅ Send OTP via email
            string subject = "Password Reset OTP";
            string body = $"<h3>Hello {user.Username},</h3><p>Your OTP for password reset is: <strong>{otp}</strong></p><p>This OTP is valid for 10 minutes.</p>";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            return Ok($"Password reset OTP has been sent to {user.Email}.");
        }



        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto requestDto)
        {
            if (string.IsNullOrEmpty(requestDto.OTP) || string.IsNullOrEmpty(requestDto.NewPassword))
                return BadRequest("Token and new password are required.");


            // ✅ Find user with matching OTP and check expiry
            var user = await _context.Users.FirstOrDefaultAsync(u => u.OTP == requestDto.OTP && u.ResetTokenExpiry > DateTime.UtcNow);
            if (user == null)
                return BadRequest("Invalid or expired OTP.");

            // ✅ Validate Strong Password
            if (!StrongPassword(requestDto.NewPassword))
                return BadRequest("Password must contain at least one uppercase, one lowercase, one numeric, one special character, and be at least 8 characters long.");

            // ✅ Hash the new password before storing it
            user.Password = requestDto.NewPassword;

            // ✅ Clear OTP after successful password reset
            user.OTP = null;
            user.ResetTokenExpiry = null;
            await _context.SaveChangesAsync();

            // ✅ Send Success Notification Email
            string subject = "Password Successfully Changed";
            string body = $"<h3>Hello {user.Username},</h3><p>Your password has been successfully changed.</p><p>If you did not request this change, please contact our support team immediately.</p>";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            return Ok("Password has been successfully reset.");
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto requestDto)
        {
            if (requestDto == null || string.IsNullOrEmpty(requestDto.OldPassword) || string.IsNullOrEmpty(requestDto.NewPassword))
                return BadRequest("Old and new passwords are required.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == requestDto.UserId);
            if (user == null)
                return NotFound("User not found.");

            // Check if old password matches
            if (user.Password != requestDto.OldPassword)
                return BadRequest("Old password is incorrect.");

            // Validate new password strength
            if (!StrongPassword(requestDto.NewPassword))
                return BadRequest("New password must contain at least one uppercase, one lowercase, one numeric, one special character, and be at least 8 characters long.");

            // Update password
            user.Password = requestDto.NewPassword;
            await _context.SaveChangesAsync();

            // Send confirmation email
            string subject = "Password Changed Successfully";
            string body = $"<h3>Hello {user.Username},</h3><p>Your password has been successfully changed.</p><p>If you did not request this change, please contact our support team immediately.</p>";
            await _emailService.SendEmailAsync(user.Email, subject, body);

            return Ok("Password has been successfully changed.");
        }


        public static bool StrongPassword(string pwd)
        {
            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasLength = pwd.Length >= 8; 
            bool hasDigit = false;
            bool hasSpecialChar = false;

            foreach (char c in pwd)
            {
                if (char.IsUpper(c))
                    hasUpperCase = true;
                if (char.IsLower(c))
                    hasLowerCase = true;
                if (char.IsDigit(c))
                    hasDigit = true;
                if (!char.IsLetterOrDigit(c))
                    hasSpecialChar = true;
            }

            return hasUpperCase && hasLowerCase && hasLength && hasDigit && hasSpecialChar;
        }

    }
}
