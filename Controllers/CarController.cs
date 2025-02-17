using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.Converters;
using Car_Rental_Backend_Application.Data.ENUMS;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Data.ResponseDto_s;
using Car_Rental_Backend_Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Car_Rental_Backend_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarRentalContext _context;

        public CarController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: api/car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarResponseDto>>> GetCars()
        {
            var cars = await _context.Cars
                .Include(c => c.Bookings)
                .ToListAsync();

            var carResponseDtos = cars.Select(car => CarConverters.CarToCarResponseDto(car)).ToList();

            return Ok(carResponseDtos);
        }

        // GET: api/car/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarResponseDto>> GetCarById(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Bookings)
                .FirstOrDefaultAsync(c => c.Car_ID == id);

            if (car == null)
                return NotFound($"Car with ID {id} not found.");

            return Ok(CarConverters.CarToCarResponseDto(car));
        }

        [HttpPost]
        public async Task<ActionResult<CarResponseDto>> CreateCar(CarRequestDto carRequestDto)
        {
            if (carRequestDto == null)
                return BadRequest("Car data is required.");

            // 🔹 Convert DTO to Car entity first
            var car = CarConverters.CarRequestDtoToCar(carRequestDto);

            // 🔹 Check if a car with the same Licence_Plate already exists
            bool isLicencePlateExists = await _context.Cars
                .AnyAsync(c => c.License_Plate == car.License_Plate);

            if (isLicencePlateExists)
            {
                throw new CarAlreadyExistedException($"The Car with the Licence_Plate{car.License_Plate} is Already Exists");
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            var createdCarResponseDto = CarConverters.CarToCarResponseDto(car);

            return CreatedAtAction(nameof(GetCarById), new { id = createdCarResponseDto.Car_ID }, createdCarResponseDto);
        }



        // PUT: api/car/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarRequestDto carRequestDto)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound($"Car with ID {id} not found.");

            // Update fields from CarRequestDto
            car.Brand = carRequestDto.Brand;
            car.Model = carRequestDto.Model;
            car.Year = carRequestDto.Year;
            car.License_Plate = carRequestDto.License_Plate;
            //car.Availability_Status = carRequestDto.Availability_Status;


            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/car/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound($"Car with ID {id} not found.");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/car/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<CarResponseDto>>> GetAvailableCars()
        {
            var availableCars = await _context.Cars
                .Where(c => c.Availability_Status == "Available")
                .ToListAsync();

            if (!availableCars.Any())
                return NotFound("No available cars at the moment.");

            var carResponseDtos = availableCars.Select(CarConverters.CarToCarResponseDto).ToList();
            return Ok(carResponseDtos);
        }

        // GET: api/car/rented
        [HttpGet("booked")]
        public async Task<ActionResult<IEnumerable<object>>> GetRentedCars()
        {
            var rentedCars = await _context.Cars
                .Include(c => c.Bookings)
                .Where(c => c.Availability_Status =="Booked")
                .Select(c => new
                {
                    Car_ID = c.Car_ID,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    License_Plate = c.License_Plate,
                    Availability_Status = c.Availability_Status,
                    BookingId = c.Bookings.OrderByDescending(b => b.BookingDate)
                                          .Select(b => b.BookingId)
                                          .FirstOrDefault() // Get the latest booking ID
                })
                .ToListAsync();

            if (!rentedCars.Any())
                return NotFound("No rented cars at the moment.");

            return Ok(rentedCars);
        }
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<CarResponseDto>>> GetCarsByCategory(string category)
        {
            var cars = await _context.Cars
                .Where(c => c.Category.ToLower() == category.ToLower())
                .ToListAsync();

            if (!cars.Any())
                return NotFound($"No cars found in category '{category}'.");

            var carResponseDtos = cars.Select(CarConverters.CarToCarResponseDto).ToList();
            return Ok(carResponseDtos);
        }
        [HttpGet("city/{location}")]
        public async Task<ActionResult<IEnumerable<CarResponseDto>>> GetCarsByCity(string location)
        {
            var cars = await _context.Cars
                .Where(c => c.Location.ToLower() == location.ToLower())
                .ToListAsync();

            if (!cars.Any())
                return NotFound($"No cars available in '{location}'.");

            var carResponseDtos = cars.Select(CarConverters.CarToCarResponseDto).ToList();
            return Ok(carResponseDtos);
        }
        [HttpGet("price-range")]
        public async Task<ActionResult<IEnumerable<CarResponseDto>>> GetCarsByPriceRange([FromQuery] int minPrice, [FromQuery] int maxPrice)
        {
            if (minPrice > maxPrice)
                return BadRequest("Minimum price cannot be greater than maximum price.");

            var cars = await _context.Cars
                .Where(c => c.PricePerDay >= minPrice && c.PricePerDay <= maxPrice)
                .ToListAsync();

            if (!cars.Any())
                return NotFound($"No cars found in the price range {minPrice} - {maxPrice}.");

            var carResponseDtos = cars.Select(CarConverters.CarToCarResponseDto).ToList();
            return Ok(carResponseDtos);
        }


    }
}
