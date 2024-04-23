using Abpd6.Dto;
using Abpd6.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Abpd6.Controller;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{

    private IWarehouseRepository _warehouseRepository;

    public WarehouseController(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductWarehouseDto productWarehouseDto)
    {
        return null;
    }
}