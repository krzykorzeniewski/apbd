using Kolokwium2.Dtos;
using Kolokwium2.Models;

namespace Kolokwium2.Services;

public interface ICharactersService
{
    public Task<CharacterGetDto?> GetCharactersById(int id);
    public Task<ICollection<BackpackPostDto>> AddSlotsToCharacter(int characterId, ICollection<int> itemIds);
}