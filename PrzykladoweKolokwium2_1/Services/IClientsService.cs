using PrzykladoweKolokwium2_1.Dtos;

namespace PrzykladoweKolokwium2_1.Services;

public interface IClientsService
{
    public Task<List<ClientOrderDto>> GetClientOrders(int id); 
}