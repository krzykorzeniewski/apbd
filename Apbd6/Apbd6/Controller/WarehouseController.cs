using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Abpd6.Dto;
using Abpd6.Repository;

namespace Abpd6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToWarehouse(ProductWarehouseDto productWarehouseDto)
        {
            // Basic validation for the request
            if (productWarehouseDto == null)
            {
                return BadRequest("Invalid request");
            }
            
            try
            {
                int result = await _warehouseRepository.Add(productWarehouseDto);
                
                if (result == 0)
                {
                    return BadRequest("Invalid request");
                }

                return Created($"Warehouse/{result}", productWarehouseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}