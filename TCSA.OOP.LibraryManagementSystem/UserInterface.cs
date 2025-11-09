using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;
using TCSA.OOP.LibraryManagementSystem.Controllers;
using static TCSA.OOP.LibraryManagementSystem.Enums;

namespace TCSA.OOP.LibraryManagementSystem
{
    internal class UserInterface
    {
        private readonly BooksController _booksController = new BooksController();
        private readonly MagazineController _magazinesController = new MagazineController();
        private readonly NewspaperController _ newspaperController = new NewspaperController();

        internal void MainMenu()
        {
            while (true)
            {
                Console.Clear();

                var actionChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOption>()
                        .Title("What do you want to do next")
                        .AddChoices(Enum.GetValues<MenuAction>())
                );

                var itemTypeChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<ItemType>()
                        .Title("Select the type of item:")
                        .AddChoices(Enum.GetValues<ItemType>())
                );

                switch (actionChoice)
                {
                    case MenuOption.ViewBooks:
                        booksController.ViewItems();
                        break;
                    case MenuOption.AddBook:
                        booksController.AddItem();
                        break;
                    case MenuOption.DeleteBook:
                        booksController.DeleteItem();
                        break;
                }
            }
        }
    }
}
