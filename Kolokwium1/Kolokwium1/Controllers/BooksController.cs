using Kolokwium1.Dtos;
using Kolokwium1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private IDbRepository _dbRepository;

    public BooksController(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }
    
    [HttpGet("{id}/genres")]
    public async Task<IActionResult> GetGenres(int id)
    {
        var resBook = await _dbRepository.GetBookGenresById(id);
        if (resBook == null)
        {
            return NotFound();
        }

        return Ok(resBook);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(BookPostDto book)
    {
        var resBook = await _dbRepository.AddBookWithGenres(book);
        if (resBook == null)
        {
            return NotFound();
        }
        return Created();
    }
}