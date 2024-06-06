using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrzykladoweKolokwium2_1.Models;

[Table("Order")]
public class Order
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CreatedAt")]
    [Required]
    public DateTime CreatedAt { get; set; }

    [Column("FulfilledAt")]
    public DateTime? FulfilledAt { get; set; }

    [Column("ClientID")]
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    public Client Client { get; set; }

    [Column("StatusID")]
    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    public Status Status { get; set; }
    
    public ICollection<ProductOrder> ProductOrders { get; set; }
}