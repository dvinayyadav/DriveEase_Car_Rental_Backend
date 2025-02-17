using System.ComponentModel.DataAnnotations;
using Car_Rental_Backend_Application.Data.ENUMS;

namespace Car_Rental_Backend_Application.Data.RequestDto_s
{
    public class CarRequestDto
    {
        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        public int PricePerDay { get; set; }

        [Required]
        public string License_Plate { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Location { get; set; }

        //[Required]
        //public string Availability_Status { get; set; }


    }
}
