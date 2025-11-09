using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;


// namespace TCSA.OOP.LibraryManagementSystem
// {
//     internal class BooksController
//     {
//         internal void ViewBooks()
//         {
//             /* Spectre's MarkupLine method is useful for styling strings.
//             We'll use it as a standard to print messages to the console. */
//             AnsiConsole.MarkupLine("[yellow]List of books:[/]");

//             // Printing each book to the console with a loop
//             foreach (var book in MockDatabase.Books)
//             {
//                 AnsiConsole.MarkupLine($"- [cyan]{book.Name}[/] - [yellow]{book.Pages} pages[/]");
//             }

//             /* Having the user press a key before continuing, or the menu
//             wouldn't be visualisable */
//             AnsiConsole.MarkupLine("Press any key to continue");
//             Console.ReadKey();
//         }

//         internal void AddBook()
//         {
//             /* Spectre's Ask<> method allows us to prompt a message to grab the users input.
//             We can pass the type we expect as an answer for validation. We're storing the answer
//             in the 'title' variable.*/
//             var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book to add:");
//             var pages = AnsiConsole.Ask<int>("Enter the [green]number of pages[/] in the book:");

//             // checking if the book already exists to avoid duplication.
//             if (
//                 MockDatabase.Books.Exists(book =>
//                     book.Name.Equals(title, StringComparison.OrdinalIgnoreCase)
//                 )
//             )
//             {
//                 AnsiConsole.MarkupLine("[red]This book already exists.[/]");
//             }
//             else
//             {
//                 // If the book doesn't existt, add to the list of books.
//                 var newBook = new Book(title, pages);
//                 MockDatabase.Books.Add(newBook);
//                 AnsiConsole.MarkupLine("[green]Book added successfully.[/]");
//             }

//             AnsiConsole.MarkupLine("Press any key to continue");
//             Console.ReadKey();
//         }

//         internal void DeleteBook()
//         {
//             // checking if there are any books to delete and letting the user know
//             if (MockDatabase.Books.Count == 0)
//             {
//                 AnsiConsole.MarkupLine("[red]No books available to delete.[/]");
//                 Console.ReadKey();
//                 return;
//             }

//             /* showing a list of books a letting the user choose with arrows
//             using SelectionPrompt, similar to what we did with the menu */
//             var bookToDelete = AnsiConsole.Prompt(
//                 new SelectionPrompt<Book>()
//                     .Title("Which [red]book[/] would you like to delete?")
//                     .UseConverter(book => $"{book.Name}")
//                     .AddChoices(MockDatabase.Books)
//             );

//             /* Using the Remove method to delete a book. This method returns a boolean
//             that we can use to present a message in case of success or failure
//             */
//             if (MockDatabase.Books.Remove(bookToDelete))
//             {
//                 AnsiConsole.MarkupLine("[red]Book deleted successfully.[/]");
//             }
//             else
//             {
//                 AnsiConsole.MarkupLine("[red]Book not found.[/]");
//             }

//             AnsiConsole.MarkupLine("Press any key to continue");
//             Console.ReadKey();
//         }
//     }
// }
