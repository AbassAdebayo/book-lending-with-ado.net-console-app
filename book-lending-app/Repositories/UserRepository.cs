using System.Security.Cryptography;
using System.Text;
using book_lending_app.Entities;
using MySql.Data.MySqlClient;

namespace book_lending_app.Repositories;

public class UserRepository
{
    private readonly MySqlConnection _connection;

    public UserRepository(MySqlConnection connection)
    {
        _connection = connection;
    }

    public bool CreateUser(User user)
    {
        string createUserQuery = "insert into Users (FirstName, LastName, UserName, Password)" +
                                    "values (@FirstName, @LastName, @UserName, @Password)";
        int result = 0;

        try
        {
            _connection.Open();
            using (MySqlCommand command = new MySqlCommand(createUserQuery, _connection))
            {
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", HashPassword(user.Password));

                result = command.ExecuteNonQuery();
                return result == 1 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database Error creating student: {ex.Message}");
            throw new ApplicationException("An error occurred while creating the student record.", ex);
        }
        finally
        {
            _connection.Close();
        }
    }
    
    public bool UserNameExists(string userName)
    {
        try
        {
            string query = "Select Count(*) from Users Where UserName = @UserName";
            _connection.Open();

            using (MySqlCommand command = new MySqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error checking email existence: {ex.Message}");
            return false;
        }
        finally
        {
            _connection.Close();
        }
    }

    public bool Login(string userName, string password)
    {
        try
        {
            string loginQuery = "SELECT Password from Users WHERE UserName = @userName";

            _connection.Open();

            using (MySqlCommand command = new MySqlCommand(loginQuery, _connection))
            {
                command.Parameters.AddWithValue("@userName", userName);

                var result = command.ExecuteScalar();

                if (result == null)
                    return false;
                
                string storedHashedPassword = result.ToString();
                
                bool isPasswordValid = VerifyPassword(password, storedHashedPassword);

                return isPasswordValid ? true : false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error login in: {ex.Message}");
            return false;
        }
        finally
        {
            _connection.Close();
        }
       

    }
    
    
    
    private static string HashPassword(string password)
    {
        // Create a new SHA-256 hash object
        using (var sha256 = SHA256.Create())
        {
            // Convert the password to bytes
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Compute the hash
            var hashBytes = sha256.ComputeHash(passwordBytes);

            // Convert the hash to a hexadecimal string
            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
    
    private static bool VerifyPassword(string password, string hashedPassword)
    {
        // Hash the provided password
        var hashedInput = HashPassword(password);

        // Compare the hashed input with the stored hash
        return hashedInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
    }
}
