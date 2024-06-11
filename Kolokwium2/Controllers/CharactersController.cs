using Kolokwium2.Exceptions;
using Kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{

    private readonly ICharactersService _service;

    public CharactersController(ICharactersService service)
    {
        _service = service;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharactersById(int id)
    {
        var res = await _service.GetCharactersById(id);
        if (res == null)
        {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpPost("{id}/backpackslots")]
    public async Task<IActionResult> AddSlotsToCharacter(int id, [FromBody] ICollection<int> itemIds)
    {
        try
        {
            var res = await _service.AddSlotsToCharacter(id, itemIds);
            return Created("", res);
        }
        catch (ItemNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}