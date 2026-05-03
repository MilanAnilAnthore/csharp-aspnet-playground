using System;
using System.Threading.Tasks;
using LibraryManager.Services;

namespace LibraryManager.UI.BorrowUi
{
    public class BorrowMain
    {
        public static async Task BorrowMainMenu(LibraryService service)
        {
            Console.Clear();
            Console.WriteLine("── BORROW BOOK ─-\n");

            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine() ?? "";

            Console.Write("Enter Member ID: ");
            string memberIdStr = Console.ReadLine() ?? "";

            Console.Write("Enter Borrowing Days: ");
            string borrowDays = Console.ReadLine() ?? "";
            try
            {
                if (int.TryParse(borrowDays, out int days))
                {
                    await service.BorrowBook(isbn, memberIdStr, DateTime.Now.AddDays(days));
                    Console.WriteLine("Book borrowed successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid number of days.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}