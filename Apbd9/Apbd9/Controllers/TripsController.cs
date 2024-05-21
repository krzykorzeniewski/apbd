using Apbd9.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apbd9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private ITripsService _service;

    public TripsController(ITripsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllTrips());
    }
}