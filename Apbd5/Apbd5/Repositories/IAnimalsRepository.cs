using Apbd5.DTOs;

namespace Apbd5.Repositories;

public interface IAnimalsRepository
{
    public AnimalGetDto? GetById(int id);
    public int Add(AnimalPostPutDto animal);
    public IEnumerable<AnimalGetDto> GetAll(string orderBy);
    public int Update(int id, AnimalPostPutDto animal);
    public int Delete(int id);
} 