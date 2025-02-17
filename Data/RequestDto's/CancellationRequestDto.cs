using System;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Backend_Application.Data.DTOs
{
    public class CancellationRequestDto
    {
        [Required]
        public int Booking_ID { get; set; } 

        [Required]
        public DateTime Cancellation_Date { get; set; } 

        public string Reason { get; set; } 
    }
}
