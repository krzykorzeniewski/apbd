using Kolokwium1.Dtos;

namespace Kolokwium1.Repositories;

public interface IDbRepository
{
    public Task<BookGetDto?> GetBookGenresById(int id);
    public Task<BookPostDto?> AddBookWithGenres(BookPostDto book);
}