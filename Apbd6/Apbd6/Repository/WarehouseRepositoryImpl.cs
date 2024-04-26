using System.Data.SqlClient;
using Abpd6.Dto;

namespace Abpd6.Repository
{
    public class WarehouseRepositoryImpl : IWarehouseRepository
    {
        private IConfiguration _configuration;

        public WarehouseRepositoryImpl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(ProductWarehouseDto productWarehouseDto)
        {
            await using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
            await sqlConnection.OpenAsync();
            await using var transaction = sqlConnection.BeginTransaction();

            try
            {
                if (!await CheckIfProductExists(productWarehouseDto.IdProduct, sqlConnection, transaction) ||
                    !await CheckIfWarehouseExists(productWarehouseDto.IdWarehouse, sqlConnection, transaction) ||
                    !CheckAmountLargerThanZero(productWarehouseDto.Amount))
                {
                    return 0;
                }

                var orderId = await FetchOrderId(productWarehouseDto.IdProduct, productWarehouseDto.Amount, 
                    productWarehouseDto.CreatedAt, sqlConnection, transaction);
                if (orderId == null || !await CheckOrderCreationDate(orderId.Value, productWarehouseDto.CreatedAt, 
                        sqlConnection, transaction))
                {
                    return 0;
                }

                if (!await CheckIfOrderCompleted(orderId.Value, sqlConnection, transaction))
                {
                    UpdateOrderFullfilledAtDate(orderId.Value, sqlConnection, transaction);
                    return await InsertIntoDb(productWarehouseDto, orderId, sqlConnection, transaction);
                }

                return 0;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<int> InsertIntoDb(ProductWarehouseDto productWarehouseDto, int? orderId, SqlConnection sqlConnection,
            SqlTransaction transaction)
        {
            var currDate = DateTime.Now;
            var productPrice = await FetchProductPrice(productWarehouseDto.IdProduct, sqlConnection, transaction);
            var sqlCommand = new SqlCommand(
                "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES " +
                "(@Warehouse, @Product, @Order, @Amount, @Price, @CreatedAt); SELECT SCOPE_IDENTITY();",
                sqlConnection, transaction);

            sqlCommand.Parameters.AddWithValue("@Warehouse", productWarehouseDto.IdWarehouse);
            sqlCommand.Parameters.AddWithValue("@Product", productWarehouseDto.IdProduct);
            sqlCommand.Parameters.AddWithValue("@Order", orderId);
            sqlCommand.Parameters.AddWithValue("@Amount", productWarehouseDto.Amount);
            sqlCommand.Parameters.AddWithValue("@Price", productWarehouseDto.Amount * productPrice);
            sqlCommand.Parameters.AddWithValue("@CreatedAt", currDate);

            var result = await sqlCommand.ExecuteScalarAsync();

            await transaction.CommitAsync();
            
            return Convert.ToInt32(result);
        }

        private async Task<Decimal> FetchProductPrice(int idProduct, SqlConnection sqlConnection, 
            SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand("SELECT Price FROM Product WHERE IdProduct = @Id", sqlConnection,
                transaction);
            sqlCommand.Parameters.AddWithValue("@Id", idProduct);

            await using var reader = await sqlCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.GetDecimal(0);
            }
            throw new InvalidOperationException("Product not found.");
        }

        private async void UpdateOrderFullfilledAtDate(int orderId, SqlConnection sqlConnection, SqlTransaction transaction)
        {
            var currDate = DateTime.Now;
            var sqlCommand = new SqlCommand("UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdOrder = @Id",
                sqlConnection, transaction);
            sqlCommand.Parameters.AddWithValue("@Id", orderId);
            sqlCommand.Parameters.AddWithValue("@FulfilledAt", currDate);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        private async Task<bool> CheckIfOrderCompleted(int orderId, SqlConnection sqlConnection, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @Id", 
                sqlConnection, transaction);
            sqlCommand.Parameters.AddWithValue("@Id", orderId);

            var result = (int)await sqlCommand.ExecuteScalarAsync();
            return result > 0;
        }

        private async Task<int?> FetchOrderId(int idProduct, int amount, DateTime creationDate, SqlConnection sqlConnection, 
            SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(
                "SELECT IdOrder FROM [Order] WHERE IdProduct = @Id AND Amount = @Amount AND CreatedAt < @CreatedAt",
                sqlConnection, transaction);
            sqlCommand.Parameters.AddWithValue("@Id", idProduct);
            sqlCommand.Parameters.AddWithValue("@Amount", amount);
            sqlCommand.Parameters.AddWithValue("@CreatedAt", creationDate);

            await using var reader = await sqlCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.GetInt32(0);
            }
            return null;
        }

        private async Task<bool> CheckOrderCreationDate(int orderId, DateTime creationDate, SqlConnection sqlConnection, 
            SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand(
                "SELECT COUNT(*) FROM [Order] WHERE IdOrder = @Id AND CreatedAt < @CreatedAt",
                sqlConnection, transaction);
            sqlCommand.Parameters.AddWithValue("@Id", orderId);
            sqlCommand.Parameters.AddWithValue("@CreatedAt", creationDate);

            var result = (int)await sqlCommand.ExecuteScalarAsync();
            return result > 0;
        }

        private bool CheckAmountLargerThanZero(int amount)
        {
            return amount > 0;
        }

        private async Task<bool> CheckIfWarehouseExists(int idWarehouse, SqlConnection sqlConnection, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @Id", sqlConnection,
                transaction);
            sqlCommand.Parameters.AddWithValue("@Id", idWarehouse);

            var result = (int)await sqlCommand.ExecuteScalarAsync();
            return result > 0;
        }

        private async Task<bool> CheckIfProductExists(int idProduct, SqlConnection sqlConnection, SqlTransaction transaction)
        {
            var sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Product WHERE IdProduct = @Id", sqlConnection,
                transaction);
            sqlCommand.Parameters.AddWithValue("@Id", idProduct);

            var result = (int)await sqlCommand.ExecuteScalarAsync();
            return result > 0;
        }
    }
}
