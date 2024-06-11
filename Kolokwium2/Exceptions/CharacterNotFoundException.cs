namespace Kolokwium2.Exceptions;

public class CharacterNotFoundException : Exception
{
    public CharacterNotFoundException(string? message) : base(message)
    {
    }
}