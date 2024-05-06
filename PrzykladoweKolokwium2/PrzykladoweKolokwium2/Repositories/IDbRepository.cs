using PrzykladoweKolokwium2.Dtos;

namespace PrzykladoweKolokwium2.Repositories;

public interface IDbRepository
{
    public Task<AnimalGet?> GetAnimalById(int id);
    public Task<AnimalPost?> AddAnimal(AnimalPost animal);
}