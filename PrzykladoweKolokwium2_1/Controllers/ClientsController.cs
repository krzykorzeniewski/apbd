using Microsoft.AspNetCore.Mvc;
using PrzykladoweKolokwium2_1.Services;

namespace PrzykladoweKolokwium2_1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{

    private IClientsService _service;

    public ClientsController(IClientsService service)
    {
        _service = service;
    }

    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetClientOrders(int id)
    {
        var res = await _service.GetClientOrders(id);
        if (res.Capacity == 0)
        {
            return NotFound();
        }
        return Ok(res);
    }
    
}