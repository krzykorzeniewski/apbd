using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Apbd10.Models;

[Table("Products_Categories")]
[PrimaryKey(nameof(IdProduct), nameof(IdCategory))]
public class ProductCategory
{
    [Column("FK_product")]
    public int IdProduct { get; set; }
    public Product Product { get; set; }
    
    [Column("FK_category")]
    public int IdCategory { get; set; }
    public Category Category { get; set; }
}