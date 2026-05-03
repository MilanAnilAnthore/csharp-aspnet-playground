using LibraryManager.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;
using LibraryManager.Models.Members;

namespace LibraryManager.Models.Books
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(PhysicalBook), typeDiscriminator: "physical")]
    [JsonDerivedType(typeof(EBook), typeDiscriminator: "ebook")]
    public abstract class Book : IIdentifiable, IBorrowable
    {
        public string Id => ISBN;
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Genre { get; set; } = "";
        public string ISBN { get; set; } = "";
        public bool IsCheckedOut { get; set; } = false;
        public Guid? CurrentBorrowerId { get; set; }
        public DateTime? DueDate { get; set; }

        protected string GetBaseDetails()
        {
            return $"[{ISBN}] - {Title} - Author: {Author} - Genre: {Genre}";
        }

        public abstract string GetDetails();

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
