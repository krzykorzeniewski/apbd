using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

[Table("Items")]
public class Item
{
    [Key]
    [Column("PK")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }

    [Column("weight")]
    [Required]
    public int Weight { get; set; }

    public ICollection<BackpackSlot> BackpackSlots { get; set; }
}