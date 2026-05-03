using LibraryManager.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.UI.BookUi
{
    public class AddPhysicalBookUi
    {
        public static List<Book> AddPhysicalBook()
        {
            while (true)
            {
                Console.WriteLine("── ADD PHYSICAL BOOK ─-");
                Console.Write("Enter ISBN: ");
                string ISBN = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Title: ");
                string Title = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Author: ");
                string Author = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Genre: ");
                string Genre = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Shelf Location: ");
                string ShelfLocation = Console.ReadLine() ?? string.Empty;

                return new List<Book>()
                {
                    new PhysicalBook { ISBN = ISBN, title = Title, author = Author, genre = Genre, shelfLocation = ShelfLocation }
                };

            }
        }
    }
}
