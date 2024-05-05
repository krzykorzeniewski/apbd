using Microsoft.AspNetCore.Mvc;
using PrzykladoweKolokwium1.repositories;

namespace PrzykladoweKolokwium1.controllers;

[ApiController]
[Route("api/groups")]
public class GroupsController : ControllerBase
{

    private IDbRepository _dbRepository;

    public GroupsController(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroup(int id)
    {
        var resGroup = await _dbRepository.GetGroupById(id);
        if (resGroup == null)
        {
            return NotFound();
        }
        return Ok(resGroup);
    }
    
}