using Apbd4.models;

namespace Apbd4.repository;

public class AnimalsRepository : IAnimalsRepository
{
    private static ICollection<Animal> _animals = new List<Animal>
    {
        new Animal
        {
            Id = 1,
            Name = "Franek",
            Category = "Rekin Młot",
            Weight = 5000.0,
            Color = "Niebieskoszary"
        }, 
        new Animal
        {
            Id = 2,
            Name = "Jarek", 
            Category = "Jamnik",
            Weight = 10.5,
            Color = "Brązowy"
        },
        new Animal
        {
            Id = 3,
            Name = "Jazzy",
            Category = "Labrador Retriever",
            Weight = 30.0,
            Color = "Ciasteczkowy"
        }
    };

    public ICollection<Animal> GetAll()
    {
        return _animals;
    }
}