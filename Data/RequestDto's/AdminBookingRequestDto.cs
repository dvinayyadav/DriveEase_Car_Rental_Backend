using System;
using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Backend_Application.Data.RequestDto_s
{
    public class AdminBookingRequestDto
    {
        [Required]
        public int User_ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int Car_ID { get; set; }

        [Required]
        public string CarDetails { get; set; }

        [Required]
        public DateOnly BookingDate { get; set; }

        [Required]
        public DateOnly PickupDate { get; set; }

        [Required]
        public DateOnly ReturnDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
    }
}
