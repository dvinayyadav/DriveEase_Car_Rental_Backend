namespace Car_Rental_Backend_Application.Exceptions
{ 
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}
