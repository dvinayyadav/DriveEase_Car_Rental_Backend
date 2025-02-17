using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Backend_Application.Data.Entities
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; } 

        [Required]
        [ForeignKey("User")]
        public int User_ID { get; set; } 
        public User User { get; set; } 
        [Required]
        [ForeignKey("Car")]
        public int Car_ID { get; set; } 
        public Car Car { get; set; }

        [Required]
        public DateOnly BookingDate { get; set; }

        [Required]
        public DateOnly PickupDate { get; set; }

        [Required]
        public DateOnly ReturnDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

       
        public ICollection<Cancellation> Cancellations { get; set; } = new List<Cancellation>();
        public string UserName { get; internal set; }
        public string CarDetails { get; internal set; }
    }
}
