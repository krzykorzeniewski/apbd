using System.ComponentModel.DataAnnotations;

namespace PrzykladoweKolokwium2_1.Dtos;

public class ProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int amount { get; set; }
}