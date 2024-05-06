using Microsoft.AspNetCore.Mvc;
using PrzykladoweKolokwium2.Dtos;
using PrzykladoweKolokwium2.Repositories;

namespace PrzykladoweKolokwium2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private IDbRepository _dbRepository;

    public AnimalsController(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnimal(int id)
    {
        var resAnimal = await _dbRepository.GetAnimalById(id);
        if (resAnimal == null)
        {
            return NotFound();
        }
        return Ok(resAnimal);
    }

    [HttpPost]
    public async Task<IActionResult> AddAnimal(AnimalPost animal)
    {
        var resAnimal = await _dbRepository.AddAnimal(animal);
        if (resAnimal == null)
        {
            return NotFound();
        }

        return Created();
    }

}