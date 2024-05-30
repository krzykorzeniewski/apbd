using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Apbd10.Models;

[Table("Roles")]
public class Role
{
    [Key]
    [Column("PK_role")]
    public int IdRole { get; set; }

    [Column("name")]
    [MaxLength(100)]
    [Required]
    public string Name { get; set; }

    public ICollection<Account> Accounts { get; set; }
}