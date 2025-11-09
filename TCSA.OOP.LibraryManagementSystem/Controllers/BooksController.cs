using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;
using TCSA.OOP.LibraryManagementSystem;
using TCSA.OOP.LibraryManagementSystem.Controllers;
using TCSA.OOP.LibraryManagementSystem.models;

namespace TCSA.OOP.LibraryManagementSystem.Controllers
{
    internal class BooksController : IBaseController
    {
        public void ViewItems()
        {
            var table = new Table();
            table.Border(TableBorder.Rounded);

            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[yellow]Title[/]");
            table.AddColumn("[yellow]Author[/]");
            table.AddColumn("[yellow]Category[/]");
            table.AddColumn("[yellow]Location[/]");
            table.AddColumn("[yellow]Pages[/]");

            // Filtering only items of the book type
            var books = MockDatabase.LibraryItems.OfType<Book>();

            foreach (var book in books)
            {
                table.AddRow(
                    book.Id.ToString(),
                    $"[cyan]{book.Name}[/]",
                    $"[cyan]{book.Author}[/]",
                    $"[green]{book.Category}[/]",
                    $"[blue]{book.Location}[/]",
                    book.Pages.ToString()
                );
            }

            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("Press any key to continue.");
            Console.ReadKey();
        }

        public void AddItem()
        {
            var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book to add:");
            var author = AnsiConsole.Ask<string>("Enter the [green]author[/] of the book:");
            var category = AnsiConsole.Ask<string>("Enter the [green]category[/] of the book:");
            var location = AnsiConsole.Ask<string>("Enter the [green]location[/] of the book:");
            var pages = AnsiConsole.Ask<int>("Enter the [green]number of pages[/] in the book:");

            if (
                MockDatabase
                    .LibraryItems.OfType<Book>()
                    .Any(book => book.Name.Equals(title, StringComparison.OrdinalIgnoreCase))
            )
            {
                AnsiConsole.MarkupLine("[red]This book already exists.[/]");
            }
            else
            {
                var newBook = new Book(
                    MockDatabase.LibraryItems.Count + 1,
                    title,
                    author,
                    category,
                    location,
                    pages
                );
                MockDatabase.LibraryItems.Add(newBook);
                AnsiConsole.MarkupLine("[green]Book added successfully![/]");
            }

            AnsiConsole.MarkupLine("Press any key to continue");
            Console.ReadKey();
        }

        public void DeleteItem()
        {
            var books = MockDatabase.LibraryItems.OfType<Book>().ToList();

            if (books.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No books available to delete.[/]");
                Console.ReadKey();
                return;
            }

            var bookToDelete = AnsiConsole.Prompt(
                new SelectionPrompt<Book>()
                    .Title("Select a [red]book[/] to delete:")
                    .AddChoices(books)
            );

            if (MockDatabase.LibraryItems.Remove(bookToDelete))
            {
                AnsiConsole.MarkupLine("[red]Book deleted successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Book not found.[/]");
            }

            AnsiConsole.MarkupLine("Press Any Key to Continue.");
            Console.ReadKey();
        }
    }
}
