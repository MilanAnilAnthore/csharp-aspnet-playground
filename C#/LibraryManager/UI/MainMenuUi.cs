using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.UI
{
    public class MainMenuUi
    {
        public static int MainMenu()
        {
            while (true)
            {
                Console.Write("""
                    ╔════════════════════════════════════╗
                    ║       LIBRARY MANAGER v1.0         ║
                    ╚════════════════════════════════════╝

                    1. Book Management
                    2. Member Management
                    3. Borrowing
                    4. Search Books
                    5. Reports
                    0. Exit

                    Select an option:
                    """);


                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if(choice >=0 && choice <= 5)
                    {
                        return choice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 0 and 5.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 5.");
                }
                
            }
        }
    }
}
