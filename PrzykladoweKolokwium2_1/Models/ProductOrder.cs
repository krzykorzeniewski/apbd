using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PrzykladoweKolokwium2_1.Models;

[Table("Product_Order")]
[PrimaryKey(nameof(ProductId), nameof(OrderId))]
public class ProductOrder
{
    [Column("ProductID")]
    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Column("OrderID")]
    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }
    public Order Order { get; set; }

    [Column("Amount")]
    [Required]
    public int Amount { get; set; }
}