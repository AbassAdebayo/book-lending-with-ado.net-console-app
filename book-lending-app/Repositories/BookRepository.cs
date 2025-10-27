using book_lending_app.Entities;
using MySql.Data.MySqlClient;
using static book_lending_app.Entities.Book;

namespace book_lending_app.Repositories;

public class BookRepository
{
    private readonly MySqlConnection _connection;
    private readonly UserRepository _userRepository;

    public BookRepository(MySqlConnection connection)
    {
        _connection = connection;
        _userRepository = new UserRepository(connection);
    }
    
    public bool CreateBook(Book book)
    {
        string createBookQuery = "insert into Books (Title, Author, Description, Status, DateCreated)" +
                                 "values (@Title, @Author, @Description, @Status, @DateCreated)";
        int result = 0;

        try
        {
            _connection.Open();
            using (MySqlCommand command = new MySqlCommand(createBookQuery, _connection))
            {
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@Status", book.Status.ToString());
                command.Parameters.AddWithValue("@DateCreated", DateTime.UtcNow);

                result = command.ExecuteNonQuery();
                return result == 1 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database Error creating book: {ex.Message}");
            throw new ApplicationException("An error occurred while creating the book record.", ex);
        }
        finally
        {
            _connection.Close();
        }
    }

    public void BorrowBook(string borrowerCode, string title, string authorName)
    {
        if (!BookExists(title, authorName))
        {
            Console.WriteLine("Book does not exist.");
            return;
        }
        
        if (!_userRepository.GetUserByBorrowerCode(borrowerCode))
        {
            Console.WriteLine("Invalid borrower code.");
            return;
        }

        string borrowBookQuery = "UPDATE Books SET Status = @Status, DateBorrowed = @DateBorrowed, DateToBeReturned = @DateToBeReturned WHERE Title = @Title AND Author = @Author AND Status = @AvailableStatus";
        try
        {
            _connection.Open();
            using (MySqlCommand command = new MySqlCommand(borrowBookQuery, _connection))
            {
                command.Parameters.AddWithValue("@Status", BookStatus.Borrowed.ToString());
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Author", authorName);
                command.Parameters.AddWithValue("@DateBorrowed", DateTime.UtcNow);
                command.Parameters.AddWithValue("@DateToBeReturned", DateTime.UtcNow.AddDays(2));
                command.Parameters.AddWithValue("@AvailableStatus", BookStatus.Available.ToString());
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Book borrowed successfully.");
                }
                else
                {
                    Console.WriteLine("Book is not available for borrowing.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database Error borrowing book: {ex.Message}");
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