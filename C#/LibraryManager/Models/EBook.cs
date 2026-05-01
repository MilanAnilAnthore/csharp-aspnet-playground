using LibraryManager.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.Repository;

namespace LibraryManager.Models
{
    public class EBook : Book, IBorrowable
    {
        public decimal fileSize;
        public string URL = "";
        public override string GetDetails()
        {
            return $"{GetBaseDetails()} - fileSize:{fileSize}, URL:{URL}";
        }


        public void Borrow(Member member, DateTime dueDate)
        {
            if (IsCheckedOut)
            {
                throw new InvalidOperationException("Book is already borrowed");
            }

            IsCheckedOut = true;


            CurrentBorrowerId = member.memberID;
            DueDate = dueDate;

            member.BorrowedBookIds.Add(this.ISBN);
        }

        public void ReturnBook(Member member)
        {

            if (!IsCheckedOut)
            {
                throw new InvalidOperationException("Book is not currently borrowed");
            }

            if (DateTime.Now > DueDate)
            {
                int daysOverdue = (DateTime.Now - DueDate.Value).Days;
                Console.WriteLine($"Returned late by {daysOverdue} days!");
            }


            IsCheckedOut = false;
            CurrentBorrowerId = null;
            DueDate = null;


            member.BorrowedBookIds.Remove(this.ISBN);
        }
    }
}
