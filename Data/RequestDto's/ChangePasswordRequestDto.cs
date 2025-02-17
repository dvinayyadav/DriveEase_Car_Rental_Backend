namespace Car_Rental_Backend_Application.Data.RequestDto_s
{
    public class ChangePasswordRequestDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
