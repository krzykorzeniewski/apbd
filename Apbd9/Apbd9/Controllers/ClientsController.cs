using Apbd9.Exceptions;
using Apbd9.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apbd9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var res = await _clientsService.DeleteById(id);
            if (res)
            { 
                return NoContent();
            }
            return NotFound();
        }
        catch (BadRequestException e)
        {
            return BadRequest();
        }
    }
}