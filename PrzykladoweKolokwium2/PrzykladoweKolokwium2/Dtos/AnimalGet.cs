namespace PrzykladoweKolokwium2.Dtos;

public class AnimalGet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime AdmissionDate { get; set; }
    public OwnerGet? Owner { get; set; }
    public List<ProcedureGet>? Procedures { get; set; }
}