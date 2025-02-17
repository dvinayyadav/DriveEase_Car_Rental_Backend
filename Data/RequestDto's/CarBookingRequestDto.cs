using System.ComponentModel.DataAnnotations;

namespace Car_Rental_Backend_Application.Data.RequestDto_s
{
    public class CarBookingRequestDto
    {
        [Required]
        public int Car_ID { get; set; }


        [Required]
        public DateOnly PickupDate { get; set; }

        [Required]
        public DateOnly ReturnDate { get; set; }
    }
}
