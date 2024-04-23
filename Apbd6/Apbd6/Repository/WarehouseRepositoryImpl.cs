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
    
    public async void Add(ProductWarehouseDto productWarehouseDto)
    {
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            
        }
    }
}