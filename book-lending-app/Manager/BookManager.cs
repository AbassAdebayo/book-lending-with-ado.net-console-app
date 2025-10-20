using book_lending_app.Entities;
using book_lending_app.Repositories;

namespace book_lending_app.Manager;

public class BookManager
{
    private readonly BookRepository _bookRepository;

    public BookManager(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public void AddBook()
    {
        Console.WriteLine("Enter book title: ");
        string bookTitle = Console.ReadLine();
        
        Console.WriteLine("Enter book author: ");
        string bookAuthor = Console.ReadLine();
        
        Console.WriteLine("Enter book description: ");
        string bookDescription = Console.ReadLine();
        
        
    }
}