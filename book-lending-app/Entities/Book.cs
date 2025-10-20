namespace book_lending_app.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string BookDescription { get; set; }
    public DateTime DateCreated { get; set; }

    public Book(string title, string author, string bookDescription, DateTime dateCreated)
    {
        Title = title;
        Author = author;
        BookDescription = bookDescription;
        DateCreated = dateCreated;
    }
}
