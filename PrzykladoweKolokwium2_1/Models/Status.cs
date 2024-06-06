using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzykladoweKolokwium2_1.Models;

[Table("Status")]
public class Status
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}