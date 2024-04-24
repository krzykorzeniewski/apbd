using Abpd6.Dto;

namespace Abpd6.Repository;

public interface IWarehouseRepository
{
    public Task<ProductWarehouseDto> Add(ProductWarehouseDto productWarehouseDto);
}