using System;
using LibraryManager.Models.Books;
using LibraryManager.Models.Members;
using LibraryManager.Repository;
using LibraryManager.Services;
using LibraryManager.UI;
using LibraryManager.UI.BookUi;
using LibraryManager.UI.MemberUi;
using LibraryManager.UI.BorrowUi;

// Initialization
Repository<Book> book = new Repository<Book>("book.json");
Repository<Member> member = new Repository<Member>("member.json");

LibraryService service = new LibraryService(book, member);


while (true)
{
    int MainMenuChoice = MainMenuUi.MainMenu();

    switch (MainMenuChoice)
    {
        case 0:
            {
                return;
            }
        case 1:
            {
                await BookMain.BookMainMenu(service);
                break;
            }
        case 2:
            {
                await MemberMain.MemberMainMenu(service);
                break;
            }
        case 3:
            {
                await BorrowMain.BorrowMainMenu(service);
                break;
            }
    }
}
