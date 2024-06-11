using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PrzykladoweKolokwium2_1.Models;

[Table("Product")]
public class Product
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }

    [Column("Price")]
    [Precision(10, 2)]
    [Required]
    public decimal Price { get; set; }

    public ICollection<ProductOrder> ProductOrders { get; set; }
}