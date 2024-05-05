using Microsoft.AspNetCore.Mvc;
using PrzykladoweKolokwium1.repositories;

namespace PrzykladoweKolokwium1.controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{

    private IDbRepository _dbRepository;

    public StudentsController(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var res = await _dbRepository.DeleteStudentById(id);
        if (res)
        {
            return NoContent();
        }
        return NotFound();
    }
}