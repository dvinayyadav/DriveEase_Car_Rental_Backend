using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Backend_Application.Data.Entities
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Admin_ID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]  // ✅ Use VARCHAR(100) for MySQL
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "VARCHAR(255)")]  // ✅ Use VARCHAR(255) for email
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]  // ✅ Use VARCHAR(255) for passwords
        public string Password { get; set; }

       
        
    }
}
