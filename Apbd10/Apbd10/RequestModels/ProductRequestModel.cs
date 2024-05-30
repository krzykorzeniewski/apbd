using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Apbd10.RequestModels;

public class ProductRequestModel
{
    [Required]
    [MaxLength(100)]
    public string ProductName { get; set; }
    
    [Required]
    [Precision(5,2)]
    public decimal ProductWeight { get; set; }
    
    [Required]
    [Precision(5,2)]
    public decimal ProductWidth { get; set; }
    
    [Required]
    [Precision(5,2)]
    public decimal ProductHeight { get; set; }
    
    [Required]
    [Precision(5,2)]
    public decimal ProductDepth { get; set; }

    [Required]
    public ICollection<int> ProductCategories { get; set; }
}