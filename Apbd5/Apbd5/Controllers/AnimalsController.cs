using Apbd5.DTOs;
using Apbd5.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Apbd5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{

    private IAnimalsRepository _animalsRepository;

    public AnimalsController(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    [HttpGet]
    public IActionResult GetAll(string orderBy = "name")
    {
        var res = _animalsRepository.GetAll(orderBy);
        return Ok(res);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var animal = _animalsRepository.GetById(id);

        if (animal == null)
        {
            return NotFound();
        }

        return Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(AnimalPostPutDto animal)
    {
        var id = _animalsRepository.Add(animal);

        var animalToReturn = new AnimalGetDto()
        {
            IdAnimal = id,
            Description = animal.Description,
            Area = animal.Area,
            Category = animal.Category,
            Name = animal.Name
        };

        return Created($"api/Animals/{id}", animalToReturn);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnimal(int id, AnimalPostPutDto animal)
    {
        var res = _animalsRepository.Update(id, animal);
        return res == 0 ? NotFound() : NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        var res = _animalsRepository.Delete(id);
        return res == 0 ? NotFound() : NoContent();
    }
}