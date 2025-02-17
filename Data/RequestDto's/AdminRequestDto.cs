using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Backend_Application.Data.RequestDto_s
{
    public class AdminRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
