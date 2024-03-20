namespace Abpd2.Exceptions;

public class TypeMismatchException : Exception
{
    public TypeMismatchException(string? message) : base(message)
    {
    }
}