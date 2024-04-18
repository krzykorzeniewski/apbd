using System.ComponentModel.DataAnnotations;

namespace Apbd5.DTOs;

public class AnimalPostPutDto
{
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string Description { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Category { get; set; }
    
    [Required]
    [MaxLength(200)]
    public String Area { get; set; }
    
}