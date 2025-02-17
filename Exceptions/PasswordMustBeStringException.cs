namespace Car_Rental_Backend_Application.Exceptions
{
    public class PasswordMustBeStringException : Exception
    {
        public PasswordMustBeStringException(string message):base(message)
        {
            
        }
    }
}
