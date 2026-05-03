using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.Models.Books;
using LibraryManager.UI.BookUi;

namespace LibraryManager.UI.BookUi
{
    public class BookMain
    {
        public static void BookMainMenu()
        {
            while (true)
            {
                Console.Write("""
                ── BOOK MANAGEMENT ──

                1. Add Physical Book
                2. Add EBook
                3. Remove Book
                4. List All Books
                0. Back

                Select an option:
                """);

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice >= 0 && choice <= 4)
                    {
                        switch (choice)
                        {
                            case 0:
                                {
                                    break;
                                }
                            case 1:
                                {
                                    List<Book> book = AddPhysicalBookUi.AddPhysicalBook();

                                    break;
                                }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number between 0 and 4.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");
                }
            } 
        }
    }
}
