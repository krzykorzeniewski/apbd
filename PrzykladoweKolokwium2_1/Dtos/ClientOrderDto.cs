namespace PrzykladoweKolokwium2_1.Dtos;

public class ClientOrderDto
{
    public int OrderId { get; set; }
    public string ClientsLastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    public string Status { get; set; }
    public ICollection<ProductDto> Products { get; set; }
}