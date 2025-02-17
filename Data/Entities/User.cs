using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Backend_Application.Data.Entities
{
    public class User
    {

        [Key]
        public int UserId { get; set; } 

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Role { get; set; } = "User";

        public string? OTP { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
