namespace Kolokwium2.Dtos;

public class CharacterGetDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public int Money { get; set; }
    public ICollection<BackpackSlotGetDto> BackpackSlots { get; set; }
    public ICollection<TitleGetDto> Titles { get; set; }
}

public class BackpackSlotGetDto
{
    public int SlotId { get; set; }
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
}

public class TitleGetDto
{
    public string Title { get; set; }
    public DateTime AquireAt { get; set; }
}