
using book_lending_app.Manager;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;

public class Program
{
    private static UserManager _userManager;
    private static BookManager _bookManager;
    public static void Main()
    {
        string connectionString = "Server=localhost;Database=book_lending;User Id=root;Password=DefinedCodes;";
        MySqlConnection connection = new MySqlConnection(connectionString);

        _userManager = new UserManager(connection);
        _bookManager = new BookManager(connection);

        Run();




       
    }

   public static void Run()
    {
        bool start;
        do
        {
            DisplayMenu();
            start = Start();
        }
        while (start);
    }

    public static void DisplayMenu()
    {
        Console.WriteLine("Welcome to the Book Lending Application!");
        Console.WriteLine("1. Register\n2. Login\n3. Borrow Book\n4. Return Book\n5. Exit");
    }

    public static bool Start()
    {
    
        switch (Convert.ToInt32(Console.ReadLine()))
        {
            case 1:
                _userManager.AddUser();
                return true;
            case 2:
                _userManager.Login();
                return true;
            case 3:
                // _bookManager.BorrowBook();
                return true;
            case 4:
                // _bookManager.ReturnBook();
                return true;
            case 5:
                Console.WriteLine("Thank you for using the Book Lending Application. Goodbye!");
                return false;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                return true;
        }
    }
}



 

