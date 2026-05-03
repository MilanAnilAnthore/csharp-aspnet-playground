using LibraryManager.Models.Members;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Models.Books
{
    public interface IBorrowable
    {
        public void Borrow(Member member, DateTime dueDate);
        public void ReturnBook(Member member);
    }
}
