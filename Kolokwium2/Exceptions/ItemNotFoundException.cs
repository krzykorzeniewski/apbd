namespace Kolokwium2.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string? message) : base(message)
    {
    }
}