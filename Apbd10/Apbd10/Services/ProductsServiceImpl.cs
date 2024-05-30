using Apbd10.Contexts;
using Apbd10.Exceptions;
using Apbd10.Models;
using Apbd10.RequestModels;
using Microsoft.EntityFrameworkCore;

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
        foreach (var category in product.ProductCategories)
        {
            await CheckIfCategoryExistsInDb(category);
            await context.ProductCategories.AddAsync(new ProductCategory
            {
                IdProduct = newProduct.IdProduct,
                IdCategory = category
            });
        }
        await context.SaveChangesAsync();
    }

    private async Task CheckIfCategoryExistsInDb(int category)
    {
        var res = await context.Categories
            .Where(c => c.IdCategory == category).FirstOrDefaultAsync();
        if (res == null)
        {
            throw new CategoryNotFoundException($"category {category} not found");
        }
    }
}