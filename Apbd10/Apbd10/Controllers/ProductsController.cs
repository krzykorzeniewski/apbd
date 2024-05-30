using Apbd10.RequestModels;
using Apbd10.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apbd10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAndCategories(ProductRequestModel product)
    {
        try
        {
            await _productsService.AddProductAndCategories(product);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}