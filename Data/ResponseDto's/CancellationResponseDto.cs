using System;

namespace Car_Rental_Backend_Application.Data.DTOs
{
    public class CancellationResponseDto
    {
        public int Cancellation_ID { get; set; } 

        public int Booking_ID { get; set; } 

        public DateTime Cancellation_Date { get; set; } 

        public string Reason { get; set; } 
    }
}
