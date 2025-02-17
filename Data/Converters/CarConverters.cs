using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Data.ResponseDto_s;
using Car_Rental_Backend_Application.Data.ENUMS;

namespace Car_Rental_Backend_Application.Data.Converters
{
    public class CarConverters
    {
       
        public static CarResponseDto CarToCarResponseDto(Car car)
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car));

            return new CarResponseDto
            {
                Car_ID = car.Car_ID,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                PricePerDay=car.PricePerDay,
                License_Plate = car.License_Plate,
                Location = car.Location,
                Category = car.Category,
                Availability_Status = car.Availability_Status.ToString(),
                BookingIds = car.Bookings?.Select(b => b.BookingId).ToList(),
            
            };
        }

      
        public static Car CarRequestDtoToCar(CarRequestDto carRequestDto)
        {
            if (carRequestDto == null)
                throw new ArgumentNullException(nameof(carRequestDto));

           
            //if (!Enum.IsDefined(typeof(AvailabilityStatus), carRequestDto.Availability_Status))
            //{
            //    throw new ArgumentException($"Invalid Availability Status value: {carRequestDto.Availability_Status}");
            //}

            var car = new Car
            {
                Brand = carRequestDto.Brand,
                Model = carRequestDto.Model,
                Year = carRequestDto.Year,
                Location = carRequestDto.Location,
                Category = carRequestDto.Category,
                PricePerDay =carRequestDto.PricePerDay,
                License_Plate = carRequestDto.License_Plate,
                //Availability_Status = carRequestDto.Availability_Status, 
            };

            return car;
        }
    }
}
