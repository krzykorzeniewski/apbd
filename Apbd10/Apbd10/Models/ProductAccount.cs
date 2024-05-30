using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Apbd10.Models;

[Table("Shopping_Carts")]
[PrimaryKey(nameof(IdAccount), nameof(IdProduct))]
public class ProductAccount
{
    [Column("FK_account")]
    public int IdAccount { get; set; }
    public Account Account { get; set; }

    [Column("FK_product")]
    public int IdProduct { get; set; }
    public Product Product { get; set; }

    [Column("amount")]
    public int Amount { get; set; }
}