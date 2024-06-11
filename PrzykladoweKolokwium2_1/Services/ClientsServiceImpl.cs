using Microsoft.EntityFrameworkCore;
using PrzykladoweKolokwium2_1.Context;
using PrzykladoweKolokwium2_1.Dtos;
using PrzykladoweKolokwium2_1.Models;

namespace PrzykladoweKolokwium2_1.Services;

public class ClientsServiceImpl(MyContext context) : IClientsService
{
    public async Task<List<ClientOrderDto>> GetClientOrders(int id)
    {
        var res = from order in context.Orders
            join client in context.Clients on order.ClientId equals client.Id
            join status in context.Statuses on order.StatusId equals status.Id
            join product_order in context.ProductOrders on order.Id equals product_order.OrderId
            join product in context.Products on product_order.ProductId equals product.Id
            where order.ClientId.Equals(id)
            select new ClientOrderDto()
            {
                OrderId = order.Id,
                ClientsLastName = client.LastName,
                CreatedAt = order.CreatedAt,
                FulfilledAt = order.FulfilledAt,
                Status = status.Name,
                Products = new List<ProductDto>()
                {
                    new ProductDto()
                    {
                        Name = product.Name,
                        amount = product_order.Amount,
                        Price = product.Price
                    }
                }
            };
        return await res.ToListAsync();
    }
}