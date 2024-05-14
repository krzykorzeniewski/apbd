namespace Kolokwium1.Dtos;

public class BookPostDto
{
    public String Title { get; set; }
    public List<GenrePostDto> Genres { get; set; }
}