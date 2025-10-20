using book_lending_app.Entities;
using book_lending_app.Repositories;
using MySql.Data.MySqlClient;

namespace book_lending_app.Manager;

public class UserManager
{
    private readonly UserRepository _userRepository;

    public UserManager(MySqlConnection mySqlConnection)
    {
        _userRepository = new UserRepository(mySqlConnection);
    }

    public void AddUser()
    {
        Console.Write("Enter FirstName: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter LastName: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter UserName: ");
        string userName = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        // Validate username uniqueness
        if (_userRepository.UserNameExists(userName))
        {
            Console.WriteLine("Username already exists. Please try again with a different username.");
            return;
        }
        
        var newUser = new User(firstName, lastName, userName, password);
        
        //Add new user to db
        _userRepository.CreateUser(newUser);
        Console.WriteLine($"Student {newUser.FullName()} with username {newUser.UserName} has been successfully registered.");
    }

    public bool Login()
    {
        Console.Write("Enter Username: ");
        string userName = Console.ReadLine();
        
        Console.Write("Enter Password: ");
        string password = Console.ReadLine();
        
        var result = _userRepository.Login(userName, password);
        return result ? true : false;


    }
}