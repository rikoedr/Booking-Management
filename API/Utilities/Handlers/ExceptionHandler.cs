namespace API.Utilities.Handlers;

/*
 * Exception Handler adalah class yang dapat digunakan untuk
 * membuat exception.
 */

public class ExceptionHandler : Exception
{
    public ExceptionHandler(string message) : base(message) {}
}
