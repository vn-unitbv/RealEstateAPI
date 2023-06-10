namespace Infrastructure.Exceptions
{
    public class InvalidEmailFormatException: Exception
    {
        public InvalidEmailFormatException(string message) : base(message) { }
    }
}