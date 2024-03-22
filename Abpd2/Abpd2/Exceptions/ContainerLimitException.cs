namespace Abpd2.Exceptions;

public class ContainerLimitException : Exception
{
    public ContainerLimitException(string? message) : base(message)
    {
    }
}