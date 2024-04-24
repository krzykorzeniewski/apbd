using System.Data.SqlClient;
using Abpd6.Dto;

namespace Abpd6.Repository;

public class WarehouseRepositoryImpl : IWarehouseRepository
{

    private readonly IConfiguration _configuration;

    public WarehouseRepositoryImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<ProductWarehouseDto> Add(ProductWarehouseDto productWarehouseDto)
    {
        await using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            
        }

        throw new NotImplementedException();
    }
}