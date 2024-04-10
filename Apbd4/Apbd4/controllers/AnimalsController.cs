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
}