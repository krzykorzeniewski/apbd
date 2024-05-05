using System.Data.SqlClient;
using PrzykladoweKolokwium1.dtos;

namespace PrzykladoweKolokwium1.repositories;

public class DbRepositoryImpl : IDbRepository
{

    private IConfiguration _configuration;

    public DbRepositoryImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<Group?> GetGroupById(int id)
    {
        await using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await sqlConnection.OpenAsync();
        await using var sqlCommand = new SqlCommand("SELECT g.id, g.name FROM Groups g WHERE g.id = @Id", 
            sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Id", id);

        var reader = await sqlCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var resGroup = new Group
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                StudentsIds = await GetStudentsAssignedToGroup(id)
            };
            return resGroup;
        }

        return null;
    }

    public async Task<bool> DeleteStudentById(int id)
    {
        await using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await sqlConnection.OpenAsync();
        var sqlTransaction = await sqlConnection.BeginTransactionAsync();

        try
        {
            await DeleteStudentFromGroupAssignments(sqlConnection, (SqlTransaction)sqlTransaction, id);
            var res = await DeleteStudentFromDb(sqlConnection, (SqlTransaction)sqlTransaction, id);
            await sqlTransaction.CommitAsync();
            return res;
        }
        catch
        {
            await sqlTransaction.RollbackAsync();
            throw;
        }
    }

    private async Task<bool> DeleteStudentFromDb(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int id)
    {
        await using var sqlCommand = new SqlCommand("DELETE FROM Students WHERE id = @Id", sqlConnection,
            sqlTransaction);
        sqlCommand.Parameters.AddWithValue("@Id", id);
        return await sqlCommand.ExecuteNonQueryAsync() != 0;
    }
    
    private async Task DeleteStudentFromGroupAssignments(SqlConnection sqlConnection, SqlTransaction sqlTransaction,
        int id)
    {
        await using var sqlCommand = new SqlCommand("DELETE FROM GroupAssignments WHERE student_id = @Id", 
            sqlConnection, sqlTransaction);
        sqlCommand.Parameters.AddWithValue("@Id", id);
        await sqlCommand.ExecuteNonQueryAsync();
    }

    private async Task<List<int>> GetStudentsAssignedToGroup(int id)
    {
        await using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await sqlConnection.OpenAsync();
        await using var sqlCommand = new SqlCommand("SELECT ga.student_id FROM GroupAssignments ga JOIN" +
                                                    " Groups g ON g.id = ga.group_id WHERE g.id = @Id", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Id", id);

        var reader = await sqlCommand.ExecuteReaderAsync();

        var resStudents = new List<int>();
        
        while (await reader.ReadAsync())
        {
            resStudents.Add(reader.GetInt32(0));
        }

        return resStudents;
    }
}