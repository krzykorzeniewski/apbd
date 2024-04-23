using System.ComponentModel.DataAnnotations;

namespace Abpd6.Dto;

public class ProductWarehouseDto
{
    [Required]
    public int IdProduct { get; set; }
    
    [Required]
    public int IdWarehouse { get; set; }
    
    [Required]
    public int Amount { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
}