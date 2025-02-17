using Car_Rental_Backend_Application.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Backend_Application.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Cancellation> Cancellations { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Admin>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Car>()
               .HasIndex(u => u.License_Plate)
               .IsUnique();

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.User_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Car)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.Car_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cancellation>()
                .HasOne(c => c.Booking)
                .WithMany(b => b.Cancellations)
                .HasForeignKey(c => c.Booking_ID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Admin>().HasData(
       new Admin
       {
           Admin_ID = 1,
           Username = "devaravinay698",
           Email = "devaravinay698.com",
           Password = "Vinay@123", 
       },
       new Admin
       {
           Admin_ID = 2,
           Username = "narasimhagorla45",
           Email = "narasimhagorla45@gmail.com",
           Password = "Narasimha@123",
       },
           new Admin
           {
               Admin_ID = 3,
               Username = "rupeshsanagala523",
               Email = "rupeshsanagala523@gmail.com",
               Password = "Rupesh@123",
           }
           ,
           new Admin
           {
               Admin_ID = 4,
               Username = "ajaythella0",
               Email = "ajaythella0@gmail.com",
               Password = "Ajay@123",
           }
   );

            modelBuilder.Entity<Car>().HasData(
         new Car { Car_ID = 1, Brand = "Honda", Model = "City", Year = 2022, PricePerDay = 50, License_Plate = "ABC123", Availability_Status = "Available", Category = "Sedan", Location = "Mumbai" },
new Car { Car_ID = 2, Brand = "Hyundai", Model = "Creta", Year = 2021, PricePerDay = 55, License_Plate = "XYZ456", Availability_Status = "Available", Category = "SUV", Location = "Delhi" },
new Car { Car_ID = 3, Brand = "Hyundai", Model = "i20", Year = 2023, PricePerDay = 40, License_Plate = "HYU789", Availability_Status = "Available", Category = "Hatchback", Location = "Bangalore" },
new Car { Car_ID = 4, Brand = "Mahindra", Model = "TUV 300", Year = 2020, PricePerDay = 60, License_Plate = "MH300T", Availability_Status = "Available", Category = "SUV", Location = "Chennai" },
new Car { Car_ID = 5, Brand = "Tata", Model = "Punch", Year = 2022, PricePerDay = 45, License_Plate = "TAT456", Availability_Status = "Available", Category = "Hatchback", Location = "Kolkata" },
new Car { Car_ID = 6, Brand = "Suzuki", Model = "Celerio", Year = 2021, PricePerDay = 35, License_Plate = "SUZ123", Availability_Status = "Available", Category = "Hatchback", Location = "Hyderabad" },
new Car { Car_ID = 7, Brand = "Tata", Model = "Tiago", Year = 2022, PricePerDay = 38, License_Plate = "TIG789", Availability_Status = "Available", Category = "Hatchback", Location = "Pune" },
new Car { Car_ID = 8, Brand = "Toyota", Model = "Corolla", Year = 2019, PricePerDay = 55, License_Plate = "TOY999", Availability_Status = "Available", Category = "Sedan", Location = "Ahmedabad" },
new Car { Car_ID = 9, Brand = "Mahindra", Model = "Bolero", Year = 2020, PricePerDay = 50, License_Plate = "BOL345", Availability_Status = "Available", Category = "SUV", Location = "Jaipur" },
new Car { Car_ID = 10, Brand = "Chevrolet", Model = "Malibu", Year = 2018, PricePerDay = 65, License_Plate = "CHEV789", Availability_Status = "Available", Category = "Luxury", Location = "Surat" },
new Car { Car_ID = 11, Brand = "Maruti Suzuki", Model = "Ertiga", Year = 2022, PricePerDay = 50, License_Plate = "ERT123", Availability_Status = "Available", Category = "MUV", Location = "Lucknow" },
new Car { Car_ID = 12, Brand = "Honda", Model = "Civic", Year = 2021, PricePerDay = 70, License_Plate = "CIV567", Availability_Status = "Available", Category = "Sedan", Location = "Nagpur" },
new Car { Car_ID = 13, Brand = "Toyota", Model = "Innova", Year = 2023, PricePerDay = 80, License_Plate = "INN999", Availability_Status = "Available", Category = "MUV", Location = "Indore" },
new Car { Car_ID = 14, Brand = "Jeep", Model = "Compass", Year = 2020, PricePerDay = 75, License_Plate = "JEEP123", Availability_Status = "Available", Category = "SUV", Location = "Patna" },
new Car { Car_ID = 15, Brand = "Kia", Model = "Seltos", Year = 2022, PricePerDay = 60, License_Plate = "KIA456", Availability_Status = "Available", Category = "SUV", Location = "Bhopal" },
new Car { Car_ID = 16, Brand = "Mahindra", Model = "Morrozo", Year = 2021, PricePerDay = 70, License_Plate = "MOR123", Availability_Status = "Available", Category = "SUV", Location = "Vadodara" },
new Car { Car_ID = 17, Brand = "Mahindra", Model = "XUV700", Year = 2022, PricePerDay = 85, License_Plate = "XUV700", Availability_Status = "Available", Category = "SUV", Location = "Ludhiana" },
new Car { Car_ID = 18, Brand = "Mahindra", Model = "XUV300", Year = 2020, PricePerDay = 55, License_Plate = "XUV300", Availability_Status = "Available", Category = "SUV", Location = "Agra" },
new Car { Car_ID = 19, Brand = "Mahindra", Model = "Thar", Year = 2023, PricePerDay = 90, License_Plate = "THAR999", Availability_Status = "Available", Category = "SUV", Location = "Nashik" },
new Car { Car_ID = 20, Brand = "Maruti Suzuki", Model = "Ciaz", Year = 2019, PricePerDay = 50, License_Plate = "CIAZ567", Availability_Status = "Available", Category = "Sedan", Location = "Meerut" },
new Car { Car_ID = 21, Brand = "Nissan", Model = "Altima", Year = 2021, PricePerDay = 75, License_Plate = "ALTIMA1", Availability_Status = "Available", Category = "Sedan", Location = "Rajkot" },
new Car { Car_ID = 22, Brand = "Tata", Model = "Altroz Dark Edition", Year = 2022, PricePerDay = 60, License_Plate = "ALT123", Availability_Status = "Available", Category = "Hatchback", Location = "Jamshedpur" },
new Car { Car_ID = 23, Brand = "Tata", Model = "Safari", Year = 2023, PricePerDay = 85, License_Plate = "SAFARI1", Availability_Status = "Available", Category = "SUV", Location = "Amritsar" },
new Car { Car_ID = 24, Brand = "Hyundai", Model = "Verna", Year = 2022, PricePerDay = 55, License_Plate = "VERNA88", Availability_Status = "Available", Category = "Sedan", Location = "Jodhpur" },
new Car { Car_ID = 25, Brand = "Volkswagen", Model = "Jetta", Year = 2019, PricePerDay = 70, License_Plate = "JET789", Availability_Status = "Available", Category = "Luxury", Location = "Dehradun" }
);

            base.OnModelCreating(modelBuilder);
        }
    }
}
