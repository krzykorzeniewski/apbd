using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Apbd10.Models;

[Table("Products")]
public class Product
{
    [Key]
    [Column("PK_product")]
    public int IdProduct { get; set; }

    [Column("name")]
    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Column("weight")]
    [Required]
    [Precision(5,2)]
    public decimal Weight { get; set; }

    [Column("width")]
    [Required]
    [Precision(5,2)]
    public decimal Width { get; set; }

    [Column("height")]
    [Required]
    [Precision(5,2)]
    public decimal Height { get; set; }

    [Column("depth")]
    [Required]
    [Precision(5,2)]
    public decimal Depth { get; set; }
    
}