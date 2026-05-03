using LibraryManager.Models.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.UI.BookUi
{
    public class AddEbookUI
    {
        public static Book AddEbook()
        {
            while (true)
            {
                Console.WriteLine("── ADD EBOOK ─-");
                Console.Write("Enter ISBN: ");
                string ISBN = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Title: ");
                string Title = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Author: ");
                string Author = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter Genre: ");
                string Genre = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter File Size (MB): ");
                string fileSizeStr = Console.ReadLine() ?? string.Empty;
                decimal fileSize = 0;
                decimal.TryParse(fileSizeStr, out fileSize);

                Console.Write("Enter URL: ");
                string URL = Console.ReadLine() ?? string.Empty;

                return new EBook { ISBN = ISBN, Title = Title, Author = Author, Genre = Genre, fileSize = fileSize, URL = URL };
            }
        }
    }
}
