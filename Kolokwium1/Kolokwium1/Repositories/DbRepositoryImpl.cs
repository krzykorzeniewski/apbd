using System.Data.Common;
using System.Data.SqlClient;
using Kolokwium1.Dtos;

namespace Kolokwium1.Repositories;

public class DbRepositoryImpl : IDbRepository
{

    private IConfiguration _configuration;

    public DbRepositoryImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<BookGetDto?> GetBookGenresById(int id)
    {
        BookGetDto bookGetDto = null;
        await using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await sqlConnection.OpenAsync();
        var sqlCommand = new SqlCommand("SELECT * FROM Books b WHERE b.pk = @Pk", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Pk", id);
        var reader = await sqlCommand.ExecuteReaderAsync();
        if (!reader.HasRows)
        {
            return null;
        }
        while (await reader.ReadAsync())
        {
            bookGetDto = new BookGetDto
            {
                Pk = reader.GetInt32(0),
                Title = reader.GetString(1),
                Genres = await GetBookGenresFromDb(id, sqlConnection)
            };
        }
        return bookGetDto;
    }

    public async Task<BookPostDto?> AddBookWithGenres(BookPostDto book)
    {
        await using var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
        await sqlConnection.OpenAsync();
        var sqlTransaction = await sqlConnection.BeginTransactionAsync();
        try
        {
            if (await EnsureGenresAreInDb(book.Genres, sqlConnection, sqlTransaction))
            {
                var newBookPk = await InsertBookToDb(book, sqlConnection, sqlTransaction);
                await InsertBookAndGenres(book ,newBookPk, sqlConnection, sqlTransaction);
                await sqlTransaction.CommitAsync();
                return book;
            }
        }
        catch (Exception)
        {
           await sqlTransaction.RollbackAsync();
           throw;
        }
        return null;
    }

    private async Task InsertBookAndGenres(BookPostDto book, int newBookPk, SqlConnection sqlConnection,
        DbTransaction sqlTransaction)
    {
        foreach (var genre in book.Genres)
        {
            var sqlCommand = new SqlCommand("INSERT INTO Books_Genres VALUES (@Book, @Genre)", sqlConnection,
                (SqlTransaction)sqlTransaction);
            sqlCommand.Parameters.AddWithValue("@Book", newBookPk);
            sqlCommand.Parameters.AddWithValue("Genre", genre.Pk);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }

    private async Task<int> InsertBookToDb(BookPostDto book, SqlConnection sqlConnection, 
        DbTransaction sqlTransaction)
    {
        var sqlCommand = new SqlCommand("INSERT INTO Books (Title) OUTPUT INSERTED.Pk VALUES (@Title)", 
            sqlConnection, (SqlTransaction)sqlTransaction);
        sqlCommand.Parameters.AddWithValue("@Title", book.Title);
        return (int)await sqlCommand.ExecuteScalarAsync();
    }

    private async Task<bool> EnsureGenresAreInDb(List<GenrePostDto> bookGenres, SqlConnection sqlConnection,
        DbTransaction sqlTransaction)
    {
        foreach (var genre in bookGenres)
        {
            var sqlCommand = new SqlCommand("SELECT * FROM Genres g WHERE g.Pk = @Pk", sqlConnection, 
                (SqlTransaction)sqlTransaction);
            sqlCommand.Parameters.AddWithValue("@Pk", genre.Pk);
            var reader = await sqlCommand.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                return false;
            }
            await reader.CloseAsync();
        }
        return true;
    }


    private async Task<List<GenreGetDto>> GetBookGenresFromDb(int id, SqlConnection sqlConnection)
    {
        var genres = new List<GenreGetDto>();
        var sqlCommand = new SqlCommand("SELECT g.name FROM books_genres bg JOIN books b ON bg.fk_book = b.pk" +
                                        " JOIN Genres g ON g.pk = bg.fk_book WHERE b.pk = @Pk;", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@Pk", id);
        var reader = await sqlCommand.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            genres.Add(new GenreGetDto
            {
                Name = reader.GetString(0)
            });
        }
        return genres;
    }
}