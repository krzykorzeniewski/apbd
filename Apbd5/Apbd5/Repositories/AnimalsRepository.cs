﻿using System.Data.SqlClient;
using Apbd5.DTOs;

namespace Apbd5.Repositories;

public class AnimalsRepository : IAnimalsRepository
{

    private IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public AnimalGetDto? GetById(int id)
    {
        using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        var sqlCommand = new SqlCommand("SELECT * FROM Animal WHERE IdAnimal = @id", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@id", id);
        sqlCommand.Connection.Open();
        
        var reader = sqlCommand.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new AnimalGetDto
        {
            IdAnimal = reader.GetInt32(0),
            Name = reader.GetString(1),
            Description = reader.GetString(2),
            Category = reader.GetString(3),
            Area = reader.GetString(4)
        };
    }

    public int Add(AnimalPostPutDto animal)
    {
        using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        
        using var sqlCommand = new SqlCommand("INSERT INTO Animal (Name, Description, Category, Area)" + 
                                        "VALUES (@Name, @Description, @Category, @Area); SELECT CAST(SCOPE_IDENTITY()" +
                                        "as int)", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Name", animal.Name);
        sqlCommand.Parameters.AddWithValue("@Description", animal.Description);
        sqlCommand.Parameters.AddWithValue("@Category", animal.Category);
        sqlCommand.Parameters.AddWithValue("@Area", animal.Area);
        sqlCommand.Connection.Open();
        
        return (int)sqlCommand.ExecuteScalar();
    }

    public IEnumerable<AnimalGetDto> GetAll(string orderBy)
    {
        var res = new List<AnimalGetDto>();
        
        string com = AppendOrderByClause(orderBy);
        
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand(com, sqlConnection);
            sqlCommand.Connection.Open();
            var reader = sqlCommand.ExecuteReader();
            
            while (reader.Read())
            {
                res.Add(new AnimalGetDto()
                {
                    IdAnimal = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Category = reader.GetString(3),
                    Area = reader.GetString(4)
                });
            }
        }
        return res;
    }

    public int Update(int id, AnimalPostPutDto animal)
    {
        using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        
        using var sqlCommand = new SqlCommand("UPDATE Animal SET Name = @Name, Description = @Description," +
                                        "Category = @Category, Area = @Area WHERE IdAnimal = @Id", sqlConnection);

        sqlCommand.Parameters.AddWithValue("@Name", animal.Name);
        sqlCommand.Parameters.AddWithValue("@Description", animal.Description);
        sqlCommand.Parameters.AddWithValue("@Category", animal.Category);
        sqlCommand.Parameters.AddWithValue("@Area", animal.Area);
        sqlCommand.Parameters.AddWithValue("@Id", id);
        sqlCommand.Connection.Open();

        return sqlCommand.ExecuteNonQuery();
    }

    public int Delete(int id)
    {
        using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));

        using var sqlCommand = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @Id", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Id", id);
        sqlCommand.Connection.Open();

        return sqlCommand.ExecuteNonQuery();
    }
    
    private string AppendOrderByClause(string orderBy)
    {
        bool safetyFlag = false;
        var columnNames = new List<string>(){ "name", "description", "category", "area" };
        
        if (columnNames.Contains(orderBy))
        {
            safetyFlag = true;
        }
        
        if (!safetyFlag)
        {
            return "SELECT * FROM Animal";
        }

        return $"SELECT * FROM Animal ORDER BY {orderBy} ASC";
    }
}