using Apbd4.models;
using Apbd4.repository;
using Microsoft.AspNetCore.Mvc;

namespace Apbd4.controllers;

[ApiController]
[Route("animals")]
public class AnimalsController : ControllerBase
{

    private IAnimalsRepository _animalsRepository;

    public AnimalsController(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }
    
    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animalsRepository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetAnimal(int id)
    {
        var res = _animalsRepository.GetById(id);
        
        if (res == null)
        {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        _animalsRepository.Add(animal);
        return Created($"animals/{animal.Id}", animal);
    }

    [HttpPut("{id}")]
    public IActionResult ReplaceAnimal(int id, Animal animal)
    {
        var removedAnimal = _animalsRepository.RemoveById(id);

        if (removedAnimal == null)
        {
            return NotFound();
        }
        
        _animalsRepository.Add(animal);
        
        return NoContent(); //czy created???
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        var removedAnimal = _animalsRepository.RemoveById(id);

        if (removedAnimal == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}