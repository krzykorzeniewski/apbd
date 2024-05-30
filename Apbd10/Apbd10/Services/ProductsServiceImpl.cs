using Apbd10.Contexts;
using Apbd10.Models;
using Apbd10.RequestModels;

namespace Apbd10.Services;

public class ProductsServiceImpl(MyDatabaseContext context) : IProductsService
{
    public async Task AddProductAndCategories(ProductRequestModel product)
    {
        var newProduct = new Product()
        {
            Name = product.ProductName,
            Depth = product.ProductDepth,
            Height = product.ProductHeight,
            Weight = product.ProductWeight,
            Width = product.ProductWidth
        };
        await context.Products.AddAsync(newProduct);
        await context.SaveChangesAsync();
    }
}