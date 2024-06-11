using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

[Table("Backpack_Slots")]
public class BackpackSlot
{
    [Key]
    [Column("PK")]
    public int Id { get; set; }

    [ForeignKey(nameof(Item))]
    [Column("FK_item")]
    public int IdItem { get; set; }
    public Item Item { get; set; }

    [ForeignKey(nameof(Character))]
    [Column("FK_character")]
    public int IdCharacter { get; set; }
    public Character Character { get; set; }
}