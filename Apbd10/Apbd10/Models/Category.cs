using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apbd10.Models;

[Table("Categories")]
public class Category
{
    [Key]
    [Column("PK_category")]
    public int IdCategory { get; set; }

    [Column("name")]
    [MaxLength(100)]
    [Required]
    public string Name { get; set; }
}