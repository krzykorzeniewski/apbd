using PrzykladoweKolokwium1.dtos;

namespace PrzykladoweKolokwium1.repositories;

public interface IDbRepository
{
    public Task<Group?> GetGroupById(int id);
    public Task<bool> DeleteStudentById(int id);
}