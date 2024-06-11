using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Models;

[Table("Characters_Titles")]
[PrimaryKey(nameof(IdCharacter), nameof(IdTitle))]
public class CharacterTitle
{
    [ForeignKey(nameof(Character))]
    [Column("FK_character")]
    public int IdCharacter { get; set; }
    public Character Character { get; set; }

    [ForeignKey(nameof(Title))]
    [Column("FK_title")]
    public int IdTitle { get; set; }
    public Title Title { get; set; }

    [Column("aquire_at")]
    public DateTime AquireAt { get; set; }
}