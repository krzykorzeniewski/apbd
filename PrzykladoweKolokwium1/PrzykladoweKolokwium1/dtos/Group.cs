namespace PrzykladoweKolokwium1.dtos;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int>? StudentsIds { get; set; }
}