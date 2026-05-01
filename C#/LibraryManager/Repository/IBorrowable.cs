using LibraryManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Repository
{
    public interface IBorrowable
    {
        public void Borrow(Member member, DateTime dueDate);
        public void ReturnBook(Member member);
    }
}
