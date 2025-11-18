// using coding_logger.controllers;
using static coding_logger.Enums;
using Spectre.Console;

namespace coding_logger
{
    public class UserInterface
    {
        CodingSessionController codingSessionController = new CodingSessionController();

        public void MainMenu()
        {
            while (true)
            {
               // Console.Clear();

                var actionChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuAction>()
                        .Title("What would you like to do?")
                        .AddChoices(Enum.GetValues<MenuAction>())
                );
                
                Console.WriteLine(actionChoice);

                switch (actionChoice)
                {
                    case MenuAction.ViewItems:
                        codingSessionController.GetAllRecords();
                        break;
                    case MenuAction.AddItem:
                        codingSessionController.Insert();
                        break;
                    case MenuAction.EditItem:
                        codingSessionController.Update();
                        break;
                    case MenuAction.DeleteItem:
                        codingSessionController.Delete();
                        break;
                }
            }
        }
    }
}