﻿using Apbd4.models;

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
            Color = "Niebieskoszary",
            Visits = new List<Visit>()
        }, 
        new Animal
        {
            Id = 2,
            Name = "Jarek", 
            Category = "Jamnik",
            Weight = 10.5,
            Color = "Brązowy",
            Visits = new List<Visit>()
        },
        new Animal
        {
            Id = 3,
            Name = "Jazzy",
            Category = "Labrador Retriever",
            Weight = 30.0,
            Color = "Ciasteczkowy",
            Visits = new List<Visit>()
        }
    };

    public ICollection<Animal> GetAll()
    {
        return _animals;
    }

    public Animal? GetById(int id)
    {
        return _animals.FirstOrDefault(e => e.Id == id);
    }

    public void Add(Animal animal)
    {
        _animals.Add(animal);
    }

    public Animal? RemoveById(int id)
    {
        var animalToRemove = _animals.FirstOrDefault(e => e.Id == id);
        
        _animals.Remove(animalToRemove);
        
        return animalToRemove;
    }

    public ICollection<Visit> GetAllVisits()
    {
        throw new NotImplementedException();
    }
}