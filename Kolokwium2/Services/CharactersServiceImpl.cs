using Kolokwium2.Context;
using Kolokwium2.Dtos;
using Kolokwium2.Exceptions;
using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Services
{
    public class CharactersServiceImpl(MyDbContext context) : ICharactersService
    {
        
        public async Task<CharacterGetDto?> GetCharactersById(int id)
        {
            var res = await (from character in context.Characters
                 where character.Id == id
                 select new
                 {
                     character.Id,
                     character.FirstName,
                     character.LastName,
                     character.MaxWeight,
                     character.Money,
                     BackpackSlots = (from backpackSlot in context.BackpackSlots
                                      join item in context.Items on backpackSlot.IdItem equals item.Id
                                      where backpackSlot.IdCharacter == character.Id
                                      select new BackpackSlotGetDto
                                      {
                                          SlotId = backpackSlot.Id,
                                          ItemName = item.Name,
                                          ItemWeight = item.Weight
                                      }).ToList(),
                     CurrentWeight = (from backpackSlot in context.BackpackSlots
                                      join item in context.Items on backpackSlot.IdItem equals item.Id
                                      where backpackSlot.IdCharacter == character.Id
                                      select item.Weight).Sum(),
                     Titles = (from characterTitle in context.CharacterTitles
                               join title in context.Titles on characterTitle.IdTitle equals title.Id
                               where characterTitle.IdCharacter == character.Id
                               select new TitleGetDto
                               {
                                   Title = title.Name,
                                   AquireAt = characterTitle.AquireAt
                               }).ToList()
                 }).SingleOrDefaultAsync();
            if (res == null)
            {
                return null;
            }
            return new CharacterGetDto
            {
                FirstName = res.FirstName,
                LastName = res.LastName,
                CurrentWeight = res.CurrentWeight,
                MaxWeight = res.MaxWeight,
                Money = res.Money,
                BackpackSlots = res.BackpackSlots,
                Titles = res.Titles
            };
        }

        public async Task<ICollection<BackpackPostDto>> AddSlotsToCharacter(int characterId, ICollection<int> itemIds)
        {
            var character = await GetCharacterById(characterId);
            await CheckIfItemsExistInDb(itemIds);
            await CheckIfCharacterHasEnoughCapacity(character, itemIds);
            var res = new List<BackpackPostDto>();
            foreach (var itemId in itemIds)
            {
                var tempSlot = new BackpackSlot
                {
                    IdCharacter = characterId,
                    IdItem = itemId
                };
                await context.BackpackSlots.AddAsync(tempSlot);
                res.Add(new BackpackPostDto()
                {
                    CharacterId = tempSlot.IdCharacter,
                    ItemId = tempSlot.IdItem,
                    SlotId = tempSlot.Id
                });
            }
            await context.SaveChangesAsync();
            return res;
        }

        private async Task<Character> GetCharacterById(int id)
        {
            var character = await context.Characters.FindAsync(id);
            if (character == null)
            {
                throw new CharacterNotFoundException($"Character {id} not found");
            }
            return character;
        }

        private async Task CheckIfItemsExistInDb(ICollection<int> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                var item = await context.Items.FindAsync(itemId);
                if (item == null)
                {
                    throw new ItemNotFoundException($"Item {itemId} not found");
                }
            }
        }

        private async Task CheckIfCharacterHasEnoughCapacity(Character character, ICollection<int> itemIds)
        {
            var items = await context.Items.Where(i => itemIds.Contains(i.Id)).ToListAsync();
            var totalNewItemsWeight = items.Sum(i => i.Weight);
            if (character.MaxWeight < character.CurrentWeight + totalNewItemsWeight)
            {
                throw new CapacityException("Maximum weight reached");
            }
        }
    }
}
