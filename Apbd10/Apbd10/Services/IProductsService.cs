using Apbd10.RequestModels;

namespace Apbd10.Services;

public interface IProductsService
{
    public Task AddProductAndCategories(ProductRequestModel product);
}