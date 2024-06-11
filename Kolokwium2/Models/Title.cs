using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium2.Models;

[Table("Titles")]
public class Title
{
    [Key]
    [Column("PK")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(100)]
    [Required]
    public string Name { get; set; }
    
    public ICollection<CharacterTitle> CharacterTitles { get; set; }
}