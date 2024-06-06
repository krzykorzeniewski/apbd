using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzykladoweKolokwium2_1.Models;

[Table("Client")]
public class Client
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("FirstName")]
    [MaxLength(50)]
    [Required]
    public string FirstName { get; set; }

    [Column("LastName")]
    [MaxLength(100)]
    [Required]
    public string LastName { get; set; }

    public ICollection<Order> Orders { get; set; }
}