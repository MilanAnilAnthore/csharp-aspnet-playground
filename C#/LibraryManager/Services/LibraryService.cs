using LibraryManager.Models.Books;
using LibraryManager.Models.Members;
using LibraryManager.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Services
{
    public class LibraryService
    {
        private Repository<Book> _bookRepo;
        private Repository<Member> _memberRepo;

        public LibraryService(Repository<Book> bookRepo, Repository<Member> memberRepo)
        {
            _bookRepo = bookRepo ?? new Repository<Book>();
            _memberRepo = memberRepo ?? new Repository<Member>();
        }

        public void AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new ArgumentException("Book must have an ISBN");
            }

            if (_bookRepo.GetAll().Any(b => b.ISBN == book.ISBN))
            {
                throw new InvalidOperationException("A book with this ISBN already exists");
            }

            _bookRepo.AddItem(book);
        }

        public void RemoveBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new ArgumentException("Book must have an ISBN");
            }

            if (_bookRepo.FindById(book.ISBN) != null) 
            {
                if (book.IsCheckedOut)
                {
                    throw new ArgumentException("Book is already been borrowed so cannot remove");
                }
                _bookRepo.RemoveItem(book);
            }

        }

        public List<Book> GetBooks()
        {
            return _bookRepo.GetAll().OrderBy(m => m.title).ToList(); ;
        }

        public List<Book> SearchBooks(string query)
        {
            List<Book> book = _bookRepo.GetAll().Where(b =>
                b.title.ToLower().Contains(query.ToLower()) ||
                b.author.ToLower().Contains(query.ToLower()) ||
                b.genre.ToLower().Contains(query.ToLower())).ToList();


            if (book.Count>0)
            {
                return book;
            }
            else
            {
                throw new ArgumentException("There is no book with this search criteria");
            }
        }

        public void RegisterMember(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.Name))
            {
                throw new ArgumentException("Member must have a name");
            }

            if (_memberRepo.GetAll().Any(m => m.Id == member.Id))
            {
                throw new InvalidOperationException("A member with this ID already exists");
            }

            _memberRepo.AddItem(member);
        }

        public void RemoveMember(string memberId)
        {
            var member = _memberRepo.FindById(memberId);
            if (member == null)
            {
                throw new ArgumentException("Member not found");
            }

            if (member.BorrowedBookIds.Count > 0)
            {
                throw new InvalidOperationException("Member has borrowed books, cannot remove");
            }

            _memberRepo.RemoveItem(member);
        }

        public List<Member> ListAllMembers()
        {
            return _memberRepo.GetAll().OrderBy(m => m.Name).ToList();
        }

        public void BorrowBook(string isbn, string memberId, DateTime dueDate)
        {
            var book = _bookRepo.FindById(isbn);

            var member = _memberRepo.FindById(memberId);
            if (member == null) throw new ArgumentException("Member not found");

            if (book is IBorrowable borrowable)
            {
                borrowable.Borrow(member, dueDate);
            }
            else
            {
                throw new InvalidOperationException("This book cannot be borrowed.");
            }
            // If data is persisted, call _bookRepo.SaveData() or similar here, assuming Repository supports it
        }

        public void ReturnBook(string isbn, string memberId)
        {
            var book = _bookRepo.FindById(isbn);

            var member = _memberRepo.FindById(memberId);
            if (member == null) throw new ArgumentException("Member not found");

            if (book.CurrentBorrowerId != member.memberID)
            {
                throw new InvalidOperationException("Book is not currently borrowed by this member.");
            }

            if (book is IBorrowable borrowable)
            {
                borrowable.ReturnBook(member);
            }
            else
            {
                throw new InvalidOperationException("This book cannot be returned.");
            }
            // Call SaveData() here
        }

        public List<Book> GetBorrowedBooks()
        {
            return _bookRepo.GetAll().Where(b => b.IsCheckedOut).ToList();
        }

        public List<Book> GetOverdueBooks()
        {
            return _bookRepo.GetAll().Where(b => 
                b.IsCheckedOut && 
                b.DueDate.HasValue && 
                b.DueDate.Value < DateTime.Now
            ).ToList();
        }
    }
}
