using System.Data.Common;
using System.Data.SqlClient;
using PrzykladoweKolokwium2.Dtos;

namespace PrzykladoweKolokwium2.Repositories;

public class DbRepositoryImpl : IDbRepository
{
    private IConfiguration _configuration;

    public DbRepositoryImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<AnimalGet?> GetAnimalById(int id)
    {
        await using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await sqlConnection.OpenAsync();
        var resAnimal = await FetchAnimalFromDb(id, sqlConnection);
        return resAnimal;
    }

    public async Task<AnimalPost?> AddAnimal(AnimalPost animal)
    {
        await using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await sqlConnection.OpenAsync();
        if (animal.Procedures == null && await CheckIfOwnerExists(animal.OwnerId, sqlConnection))
        {
            return await InsertAnimalWithoutProcedures(sqlConnection, animal);
        } 
        if (await CheckIfProcedureExists(animal.Procedures, sqlConnection) &&
                 await CheckIfOwnerExists(animal.OwnerId, sqlConnection))
        {
            return await InsertAnimalAndProcedures(sqlConnection, animal);
        }
        return null;
    }

    private async Task<AnimalPost?> InsertAnimalAndProcedures(SqlConnection sqlConnection, AnimalPost animal)
    {
        var sqlTransaction = await sqlConnection.BeginTransactionAsync();
        try
        {
            var sqlCommand = new SqlCommand("INSERT INTO Animal VALUES (@Name, @Type, @AdmissionDate, @Owner) ",
                sqlConnection, (SqlTransaction)sqlTransaction);
            sqlCommand.Parameters.AddWithValue("@Name", animal.Name);
            sqlCommand.Parameters.AddWithValue("@Type", animal.Type);
            sqlCommand.Parameters.AddWithValue("@AdmissionDate", animal.AdmissionDate);
            sqlCommand.Parameters.AddWithValue("@Owner", animal.OwnerId);
            await sqlCommand.ExecuteNonQueryAsync();
            foreach (var procedure in animal.Procedures)
            {
                var sqlCommand2 = new SqlCommand("INSERT INTO Procedure_Animal VALUES (@ProcedureId, @Animal," +
                                                 " @Date)", sqlConnection, (SqlTransaction)sqlTransaction);
                sqlCommand2.Parameters.AddWithValue("@ProcedureId", procedure.ProcedureId);
                sqlCommand2.Parameters.AddWithValue("@Animal", animal.OwnerId);
                sqlCommand2.Parameters.AddWithValue("@Date", procedure.Date);
                await sqlCommand2.ExecuteReaderAsync();
            }
        }
        catch 
        {
            await sqlTransaction.RollbackAsync();
            throw;
        }
        return animal;
    }

    private async Task<AnimalPost?> InsertAnimalWithoutProcedures(SqlConnection sqlConnection, AnimalPost animal)
    {
        var sqlTransaction = await sqlConnection.BeginTransactionAsync();
        try
        {
            var sqlCommand = new SqlCommand("INSERT INTO Animal VALUES (@Name, @Type, @AdmissionDate, @Owner) ",
                sqlConnection, (SqlTransaction)sqlTransaction);
            sqlCommand.Parameters.AddWithValue("@Name", animal.Name);
            sqlCommand.Parameters.AddWithValue("@Type", animal.Type);
            sqlCommand.Parameters.AddWithValue("@AdmissionDate", animal.AdmissionDate);
            sqlCommand.Parameters.AddWithValue("@Owner", animal.OwnerId);
            await sqlCommand.ExecuteNonQueryAsync();
        }
        catch 
        {
            await sqlTransaction.RollbackAsync();
            throw;
        }

        return animal;
    }

    private async Task<bool> CheckIfProcedureExists(List<ProcedurePost>? animalProcedures, SqlConnection sqlConnection)
    {
        foreach (var procedure in animalProcedures)
        {
            var sqlCommand = new SqlCommand("SELECT p.id FROM \"PROCEDURE\" p WHERE p.id = @Id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", procedure.ProcedureId);
            await using var reader = await sqlCommand.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                return false;
            }
        }
        return true;
    }

    private async Task<bool> CheckIfOwnerExists(int animalOwnerId, SqlConnection sqlConnection)
    {
        var sqlCommand = new SqlCommand("SELECT o.id FROM OWNER o WHERE o.id = @Id", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Id", animalOwnerId);
        await using var reader = await sqlCommand.ExecuteReaderAsync();
        Console.WriteLine(reader.HasRows);
        return reader.HasRows;
    }

    private async Task<AnimalGet?> FetchAnimalFromDb(int id, SqlConnection sqlConnection)
    {
        var sqlCommand = new SqlCommand("SELECT a.id, a.name, a.type, a.admissiondate FROM ANIMAL a WHERE" +
                                        " a.id = @Id", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Id", id);
        await using var reader = await sqlCommand.ExecuteReaderAsync();
        var resAnimal = new AnimalGet();
        if (!reader.HasRows)
        {
            return null;
        }

        while (await reader.ReadAsync())
        {
            resAnimal = new AnimalGet()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Type = reader.GetString(2),
                AdmissionDate = reader.GetDateTime(3),
                Owner = await FetchOwnerFromDb(id, sqlConnection),
                Procedures = await FetchProceduresFromDb(id, sqlConnection)
            };
        }

        return resAnimal;
    }

    private async Task<List<ProcedureGet>?> FetchProceduresFromDb(int id, SqlConnection sqlConnection)
    {
        var resProcedures = new List<ProcedureGet>();
        var sqlCommand = new SqlCommand("SELECT p.name, p.description, pa.date FROM \"Procedure\" p " +
                                        "JOIN Procedure_Animal pa ON p.id = pa.procedure_id " +
                                        "JOIN Animal a ON a.id = pa.animal_id WHERE a.id = @Id;", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Id", id);
        await using var reader = await sqlCommand.ExecuteReaderAsync();
        if (!reader.HasRows)
        {
            return null;
        }
        while (await reader.ReadAsync())
        {
            resProcedures.Add(new ProcedureGet
            {
                Name = reader.GetString(0),
                Description = reader.GetString(1),
                Date = reader.GetDateTime(2)
            });
        }

        return resProcedures;
    }

    private async Task<OwnerGet?> FetchOwnerFromDb(int id, SqlConnection sqlConnection)
    {
        OwnerGet resOwner = null;
        var sqlCommand = new SqlCommand("SELECT o.id, o.firstname, o.lastname FROM OWNER o JOIN Animal a ON" +
                                        " a.owner_id = o.id WHERE a.id = @Id", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Id", id);
        await using var reader = await sqlCommand.ExecuteReaderAsync();
        if (!reader.HasRows)
        {
            return null;
        }
        while (await reader.ReadAsync())
        {
            resOwner = new OwnerGet
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2)
            };
        }

        return resOwner;
    }
}

