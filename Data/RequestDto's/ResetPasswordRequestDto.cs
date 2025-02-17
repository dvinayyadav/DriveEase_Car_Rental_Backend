namespace Car_Rental_Backend_Application.Data.RequestDto_s
{
    public class ResetPasswordRequestDto
    {
        public string OTP { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
