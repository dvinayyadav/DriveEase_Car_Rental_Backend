using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Car_Rental_Backend_Application.Migrations
{
    /// <inheritdoc />
    public partial class ContactAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Admin_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Admin_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Car_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<int>(type: "int", nullable: false),
                    License_Plate = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Availability_Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Car_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Full_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OTP = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResetTokenExpiry = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Car_ID = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PickupDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Cars_Car_ID",
                        column: x => x.Car_ID,
                        principalTable: "Cars",
                        principalColumn: "Car_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cancellations",
                columns: table => new
                {
                    Cancellation_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Booking_ID = table.Column<int>(type: "int", nullable: false),
                    Cancellation_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancellations", x => x.Cancellation_ID);
                    table.ForeignKey(
                        name: "FK_Cancellations_Bookings_Booking_ID",
                        column: x => x.Booking_ID,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Admin_ID", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "devaravinay698.com", "Vinay@123", "devaravinay698" },
                    { 2, "narasimhagorla45@gmail.com", "Narasimha@123", "narasimhagorla45" },
                    { 3, "rupeshsanagala523@gmail.com", "Rupesh@123", "rupeshsanagala523" },
                    { 4, "ajaythella0@gmail.com", "Ajay@123", "ajaythella0" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Car_ID", "Availability_Status", "Brand", "Category", "License_Plate", "Location", "Model", "PricePerDay", "Year" },
                values: new object[,]
                {
                    { 1, "Available", "Honda", "Sedan", "ABC123", "Mumbai", "City", 50, 2022 },
                    { 2, "Available", "Hyundai", "SUV", "XYZ456", "Delhi", "Creta", 55, 2021 },
                    { 3, "Available", "Hyundai", "Hatchback", "HYU789", "Bangalore", "i20", 40, 2023 },
                    { 4, "Available", "Mahindra", "SUV", "MH300T", "Chennai", "TUV 300", 60, 2020 },
                    { 5, "Available", "Tata", "Hatchback", "TAT456", "Kolkata", "Punch", 45, 2022 },
                    { 6, "Available", "Suzuki", "Hatchback", "SUZ123", "Hyderabad", "Celerio", 35, 2021 },
                    { 7, "Available", "Tata", "Hatchback", "TIG789", "Pune", "Tiago", 38, 2022 },
                    { 8, "Available", "Toyota", "Sedan", "TOY999", "Ahmedabad", "Corolla", 55, 2019 },
                    { 9, "Available", "Mahindra", "SUV", "BOL345", "Jaipur", "Bolero", 50, 2020 },
                    { 10, "Available", "Chevrolet", "Luxury", "CHEV789", "Surat", "Malibu", 65, 2018 },
                    { 11, "Available", "Maruti Suzuki", "MUV", "ERT123", "Lucknow", "Ertiga", 50, 2022 },
                    { 12, "Available", "Honda", "Sedan", "CIV567", "Nagpur", "Civic", 70, 2021 },
                    { 13, "Available", "Toyota", "MUV", "INN999", "Indore", "Innova", 80, 2023 },
                    { 14, "Available", "Jeep", "SUV", "JEEP123", "Patna", "Compass", 75, 2020 },
                    { 15, "Available", "Kia", "SUV", "KIA456", "Bhopal", "Seltos", 60, 2022 },
                    { 16, "Available", "Mahindra", "SUV", "MOR123", "Vadodara", "Morrozo", 70, 2021 },
                    { 17, "Available", "Mahindra", "SUV", "XUV700", "Ludhiana", "XUV700", 85, 2022 },
                    { 18, "Available", "Mahindra", "SUV", "XUV300", "Agra", "XUV300", 55, 2020 },
                    { 19, "Available", "Mahindra", "SUV", "THAR999", "Nashik", "Thar", 90, 2023 },
                    { 20, "Available", "Maruti Suzuki", "Sedan", "CIAZ567", "Meerut", "Ciaz", 50, 2019 },
                    { 21, "Available", "Nissan", "Sedan", "ALTIMA1", "Rajkot", "Altima", 75, 2021 },
                    { 22, "Available", "Tata", "Hatchback", "ALT123", "Jamshedpur", "Altroz Dark Edition", 60, 2022 },
                    { 23, "Available", "Tata", "SUV", "SAFARI1", "Amritsar", "Safari", 85, 2023 },
                    { 24, "Available", "Hyundai", "Sedan", "VERNA88", "Jodhpur", "Verna", 55, 2022 },
                    { 25, "Available", "Volkswagen", "Luxury", "JET789", "Dehradun", "Jetta", 70, 2019 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Email",
                table: "Admin",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Car_ID",
                table: "Bookings",
                column: "Car_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_User_ID",
                table: "Bookings",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cancellations_Booking_ID",
                table: "Cancellations",
                column: "Booking_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_License_Plate",
                table: "Cars",
                column: "License_Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Cancellations");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
