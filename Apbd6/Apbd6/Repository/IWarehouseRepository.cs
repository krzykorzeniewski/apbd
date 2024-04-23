using Abpd6.Dto;

namespace Abpd6.Repository;

public interface IWarehouseRepository
{
    public void Add(ProductWarehouseDto productWarehouseDto);
}