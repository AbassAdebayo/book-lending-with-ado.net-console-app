using book_lending_app.Entities;
using book_lending_app.Repositories;
using MySql.Data.MySqlClient;

namespace book_lending_app.Manager;

public class BookManager
{
    private readonly BookRepository _bookRepository;

    public BookManager(MySqlConnection mySqlConnection)
    {
        _bookRepository = new BookRepository(mySqlConnection);
    }

    public void AddBook()
    {
        Console.Write("Enter book title: ");
        string bookTitle = Console.ReadLine();
        
        Console.Write("Enter book author: ");
        string bookAuthor = Console.ReadLine();
        
        Console.Write("Enter book description: ");
        string bookDescription = Console.ReadLine();
        
        if(_bookRepository.BookExists(bookTitle, bookAuthor)) Console.WriteLine("Book already exists");
        
        Book book = new Book(bookTitle, bookAuthor, bookDescription, DateTime.UtcNow);
        
        var createBook = _bookRepository.CreateBook(book);
        var result = createBook ? "Book added successfully" : "Book could not be added";
        Console.WriteLine(result);
       
    }
}