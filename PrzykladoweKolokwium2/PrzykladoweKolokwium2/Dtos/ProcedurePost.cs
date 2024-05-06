using System.ComponentModel.DataAnnotations;

namespace PrzykladoweKolokwium2.Dtos;

public class ProcedurePost
{
    [Required]
    public int ProcedureId { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
}