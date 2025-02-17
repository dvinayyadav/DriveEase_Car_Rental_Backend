namespace Car_Rental_Backend_Application.Data.RequestDto_s
{
    public class UserProfileDto
    {
        public string Username { get; set; }
        public string Email { get; set; }  // Email is usually read-only
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
