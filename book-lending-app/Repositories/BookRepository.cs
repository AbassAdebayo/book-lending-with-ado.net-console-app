using book_lending_app.Entities;
using MySql.Data.MySqlClient;

namespace book_lending_app.Repositories;

public class BookRepository
{
    private readonly MySqlConnection _connection;

    public BookRepository(MySqlConnection connection)
    {
        _connection = connection;
    }
    
    public bool CreateBook(Book book)
    {
        string createBookQuery = "insert into Books (Title, Author, BookDescription, DateCreated)" +
                                 "values (@Title, @Author, @BookDescription, @DateCreated)";
        int result = 0;

        try
        {
            _connection.Open();
            using (MySqlCommand command = new MySqlCommand(createBookQuery, _connection))
            {
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@BookDescription", book.BookDescription);
                command.Parameters.AddWithValue("@DateCreated", DateTime.UtcNow);

                result = command.ExecuteNonQuery();
                return result == 1 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database Error creating student: {ex.Message}");
            throw new ApplicationException("An error occurred while creating the book record.", ex);
        }
        finally
        {
            _connection.Close();
        }
    }
    
    public bool BookExists(string title, string author)
    {
        try
        {
            string query = "Select Count(*) from Books Where Title = @Title and Author = @Author";
            _connection.Open();

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", author);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking book existence: {ex.Message}");
            return false;
        }
        finally
        {
            _connection.Close();
        }
    }
}