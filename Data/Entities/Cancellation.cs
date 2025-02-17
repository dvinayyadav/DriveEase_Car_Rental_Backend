using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Backend_Application.Data.Entities
{
    public class Cancellation
    {
        [Key]
        public int Cancellation_ID { get; set; } 

        [Required]
        public int Booking_ID { get; set; } 
        public Booking Booking { get; set; }

        [Required]
        public DateTime Cancellation_Date { get; set; }

        public string Reason { get; set; }
    }
}
