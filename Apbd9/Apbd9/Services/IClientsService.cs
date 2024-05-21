namespace Apbd9.Services;

public interface IClientsService
{
    public Task<bool> DeleteById(int id);
}