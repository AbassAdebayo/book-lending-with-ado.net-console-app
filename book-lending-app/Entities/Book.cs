namespace book_lending_app.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    
    public BookStatus Status { get; set; }
    public DateTime DateBorrowed { get; set; }
    public DateTime DateToBeReturned { get; set; }
    public DateTime DateCreated { get; set; }


    public Book(string title, string author, string description, BookStatus bookStatus, DateTime dateCreated, DateTime dateBorrowed, DateTime dateToBeReturned)
    {
        Title = title;
        Author = author;
        Description = description;
        DateCreated = dateCreated;
        Status = bookStatus;
        DateBorrowed = dateBorrowed;
        DateToBeReturned = dateToBeReturned;
    }

    public Book(string title, string author, string description, BookStatus status)
    {
        Title = title;
        Author = author;
        Description = description;
        Status = status;
    }
   

    public enum BookStatus
    {
        Available,
        Borrowed,
        Reserved
    }
}
