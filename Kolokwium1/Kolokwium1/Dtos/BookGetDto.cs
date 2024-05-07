namespace Kolokwium1.Dtos;

public class BookGetDto
{
    public int Pk { get; set; }
    public String Title { get; set; }
    public List<GenreGetDto> Genres { get; set; }
}