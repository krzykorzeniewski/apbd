using System.ComponentModel.DataAnnotations;

namespace PrzykladoweKolokwium2.Dtos;

public class AnimalPost
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Type { get; set; }
    
    [Required]
    public DateTime AdmissionDate { get; set; }
    
    [Required]
    public int OwnerId { get; set; }
    
    public List<ProcedurePost>? Procedures { get; set; }
}