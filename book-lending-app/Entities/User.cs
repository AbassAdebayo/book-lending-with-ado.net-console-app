namespace book_lending_app.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string BorrowerCode { get; set; }


    public User(string firstName, string lastName, string userName, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Password = password;
    
    }



    public string FullName()
    {
        return $"{FirstName} {LastName}";
    }

   
}