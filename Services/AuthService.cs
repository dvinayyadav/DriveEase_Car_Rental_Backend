using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Car_Rental_Backend_Application.Services
{
    public class AuthService
    {
        private readonly CarRentalContext _context; 
        private readonly IConfiguration _config;  
        private readonly EmailService _emailService;

        public AuthService(CarRentalContext context, IConfiguration config, EmailService emailService)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));  // ✅ Fix null issue
            this._config = config ?? throw new ArgumentNullException(nameof(config));

            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        // ✅ REGISTER METHOD
        public async Task<IActionResult> Register(UserRequestDto registerDTO)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDTO.Email))
            {
                return new BadRequestObjectResult("User already exists");
            }

            var user = new User
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Address = registerDTO.Address,
                PhoneNumber = registerDTO.PhoneNumber,
                Role = "User"
            };

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, registerDTO.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // ✅ Send Confirmation Email
            string subject = "Welcome to Car Rental Service!";
            string message = $"<h3>Hello {user.Username},</h3><p>Thank you for registering with Car Rental Service.</p>";
            await _emailService.SendEmailAsync(user.Email, subject, message);


            return new OkObjectResult("User registered successfully");
        }


        // ✅ JWT TOKEN GENERATION
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["Jwt:Key"]));  // ✅ Use `this._config`
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: this._config["Jwt:Issuer"],
                audience: this._config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateJwtToken(Admin admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: this._config["Jwt:Issuer"],
                audience: this._config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IActionResult> Login(UserRequestLoginDto loginDTO)
        {
            // ✅ Check if the email/username exists in Users
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == loginDTO.Email || u.Username == loginDTO.Email);

            if (user != null)
            {
                // ✅ Verify User Password
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);

                if (result == PasswordVerificationResult.Failed)
                    return new UnauthorizedObjectResult("Invalid email or password");

                // ✅ Generate JWT Token for User
                var token = GenerateJwtToken(user);
                return new OkObjectResult(new { UserId = user.UserId, username = user.Username, token, role = "User" });
            }

            // ✅ If User not found, check Admins
            var admin = await _context.Admin
                .SingleOrDefaultAsync(a => a.Email == loginDTO.Email || a.Username == loginDTO.Email);

            if (admin != null)
            {
                // ✅ Verify Admin Password (Plain text, but hashing is recommended)
                if (admin.Password != loginDTO.Password)
                    return new UnauthorizedObjectResult("Invalid email or password");

                // ✅ Generate JWT Token for Admin
                var token = GenerateJwtToken(admin);
                return new OkObjectResult(new { UserId = admin.Admin_ID, username = admin.Username, token, role = "Admin" });
            }

            // ✅ If neither User nor Admin is found, return Unauthorized
            return new UnauthorizedObjectResult("Invalid email or password");
        }


       

    }
}
