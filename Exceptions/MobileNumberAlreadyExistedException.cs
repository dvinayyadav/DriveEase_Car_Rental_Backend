namespace Car_Rental_Backend_Application.Exceptions
{
    public class MobileNumberAlreadyExistedException : Exception
    {
        public MobileNumberAlreadyExistedException(string message) : base(message)
        {
            
        }
    }
}
