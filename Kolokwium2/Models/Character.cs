using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

[Table("Characters")]
public class Character
{
    [Key]
    [Column("PK")]
    public int Id { get; set; }

    [Column("first_name")]
    [MaxLength(50)]
    [Required]
    public string FirstName { get; set; }

    [Column("last_name")]
    [MaxLength(50)]
    [Required]
    public string LastName { get; set; }

    [Column("current_weight")]
    [Required]
    public int CurrentWeight { get; set; }

    [Column("max_weight")]
    [Required]
    public int MaxWeight { get; set; }

    [Column("money")]
    [Required]
    public int Money { get; set; }

    public ICollection<CharacterTitle> CharacterTitles { get; set; }
    
    public ICollection<BackpackSlot> BackpackSlots { get; set; }
}