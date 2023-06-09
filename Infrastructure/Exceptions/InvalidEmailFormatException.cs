namespace Infrastructure.Exceptions
{
    internal class InvalidEmailFormatException: Exception
    {
        public InvalidEmailFormatException(string message) : base(message) { }
    }
}