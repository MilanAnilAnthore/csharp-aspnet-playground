using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.Models.Books;
using LibraryManager.Services;
using LibraryManager.UI.BookUi;

namespace LibraryManager.UI.BookUi
{
    public class BookMain
    {
        public static async Task BookMainMenu(LibraryService service)
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
                                    Book book = AddPhysicalBookUi.AddPhysicalBook();
                                    await service.AddBook(book);
                                    break;
                                }
                            case 2:
                                {
                                    Book book = AddEbookUI.AddEbook();
                                    await service.AddBook(book);
                                    break;
                                }
                            case 3:
                                {
                                    while (true)
                                    {
                                        Console.Write("Enter ISBN of Book to remove: ");
                                        string isbnInput = Console.ReadLine() ?? "";
                                        if (!string.IsNullOrWhiteSpace(isbnInput))
                                        {
                                            try
                                            {
                                                await service.RemoveBook(isbnInput);
                                                break;
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Enter a valid ISBN");
                                        }
                                    }
                                    break;
                                }
                            case 4:
                                {
                                     List<Book> Allbook = await service.GetBooks();
                                    foreach (Book book in Allbook)
                                    {
                                        Console.WriteLine(book.GetDetails());
                                    }
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
