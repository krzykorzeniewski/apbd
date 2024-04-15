using Apbd4MinimalApi.model;

namespace Apbd4MinimalApi.repository;

public interface IAnimalsRepository
{
    public ICollection<Animal> GetAll();

    public Animal? GetById(int id);

    public void Add(Animal animal);

    public Animal? RemoveById(int id);
    
}