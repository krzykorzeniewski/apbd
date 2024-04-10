using Apbd4.models;

namespace Apbd4.repository;

public interface IAnimalsRepository
{
    public ICollection<Animal> GetAll();
}