namespace Car_Rental_Backend_Application.Data.ResponseDto_s
{
    public class CarBookingResponseDto
    {
       
        public int User_ID { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public int Car_ID { get; set; }
        public string CarDetails { get; set; }

        public DateOnly BookingDate { get; set; }
        public DateOnly PickupDate { get; set; }
        public DateOnly ReturnDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
