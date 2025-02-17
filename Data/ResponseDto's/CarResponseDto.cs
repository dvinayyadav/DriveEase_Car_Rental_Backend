using System.ComponentModel.DataAnnotations;
using Car_Rental_Backend_Application.Data.ENUMS;

namespace Car_Rental_Backend_Application.Data.ResponseDto_s
{
    public class CarResponseDto
    {
        public int Car_ID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public int PricePerDay { get; set; }
        public string License_Plate { get; set; }
        public string Availability_Status { get; set; }
        public string Category { get; set; }

        public string Location { get; set; }
        public List<int> BookingIds { get; set; }
    }
}
