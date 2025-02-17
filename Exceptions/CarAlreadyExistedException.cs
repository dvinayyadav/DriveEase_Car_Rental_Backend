namespace Car_Rental_Backend_Application.Exceptions
{
    public class CarAlreadyExistedException : Exception
    {

        public CarAlreadyExistedException(string message):base(message)
        {
            
        }
    }
}
