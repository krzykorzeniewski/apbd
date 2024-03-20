namespace Abpd2.Exceptions;

public class LoadLimitException : Exception
{
    public LoadLimitException(string? message) : base(message)
    {
    }
}