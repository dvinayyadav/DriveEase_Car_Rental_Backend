using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Backend_Application.Data.Entities
{
    public class Contact
    {

        [Key]
        public int ContactId { get; set; }

        [Required]
        public string Full_Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
