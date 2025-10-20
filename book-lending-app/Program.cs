// See https://aka.ms/new-console-template for more information

using book_lending_app.Manager;using MySql.Data.MySqlClient;

 UserManager _userManager;

string connectionString = "Server=localhost;Database=book_lending;User Id=root;Password=DefinedCodes;";
MySqlConnection connection = new MySqlConnection(connectionString);

_userManager = new UserManager(connection);

// _userManager.AddUser();
var result  = _userManager.Login();
Console.WriteLine(result);

