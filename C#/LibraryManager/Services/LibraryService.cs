using LibraryManager.Models.Books;
using LibraryManager.Models.Members;
using LibraryManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Services
{
    public class LibraryService
    {
        private Repository<Book> _bookRepo;
        private Repository<Member> _memberRepo;

        public LibraryService(Repository<Book> bookRepo, Repository<Member> memberRepo)
        {
            _bookRepo = bookRepo ?? new Repository<Book>("book.json");
            _memberRepo = memberRepo ?? new Repository<Member>("member.json");
        }

        public async Task AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new ArgumentException("Book must have an ISBN");
            }

            var books = await _bookRepo.GetAll();
            if (books.Any(b => b.ISBN == book.ISBN))
            {
                throw new InvalidOperationException("A book with this ISBN already exists");
            }

            await _bookRepo.AddItem(book);
        }

        public async Task RemoveBook(string ISBN)
        {
            var foundBook = await _bookRepo.FindById(ISBN);
            if (foundBook != null) 
            {
                if (foundBook.IsCheckedOut)
                {
                    throw new ArgumentException("Book is already been borrowed so cannot remove");
                }
                await _bookRepo.RemoveItem(foundBook);
            }
        }

        public async Task<List<Book>> GetBooks()
        {
            var books = await _bookRepo.GetAll();
            return books.OrderBy(m => m.Title).ToList();
        }

        public async Task<List<Book>> SearchBooks(string query)
        {
            var allBooks = await _bookRepo.GetAll();
            List<Book> book = allBooks.Where(b =>
                b.Title.ToLower().Contains(query.ToLower()) ||
                b.Author.ToLower().Contains(query.ToLower()) ||
                b.Genre.ToLower().Contains(query.ToLower())).ToList();

            if (book.Count > 0)
            {
                return book;
            }
            else
            {
                throw new ArgumentException("There is no book with this search criteria");
            }
        }

        public async Task RegisterMember(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.Name))
            {
                throw new ArgumentException("Member must have a name");
            }

            var members = await _memberRepo.GetAll();
            if (members.Any(m => m.Id == member.Id))
            {
                throw new InvalidOperationException("A member with this ID already exists");
            }

            await _memberRepo.AddItem(member);
        }

        public async Task RemoveMember(string memberId)
        {
            var member = await _memberRepo.FindById(memberId);
            if (member == null)
            {
                throw new ArgumentException("Member not found");
            }

            if (member.BorrowedBookIds.Count > 0)
            {
                throw new InvalidOperationException("Member has borrowed books, cannot remove");
            }

            await _memberRepo.RemoveItem(member);
        }

        public async Task<List<Member>> ListAllMembers()
        {
            var members = await _memberRepo.GetAll();
            return members.OrderBy(m => m.Name).ToList();
        }

        public async Task BorrowBook(string isbn, string memberId, DateTime dueDate)
        {
            var book = await _bookRepo.FindById(isbn);
            if (book == null) throw new ArgumentException("Member not found");
            var member = await _memberRepo.FindById(memberId);
            if (member == null) throw new ArgumentException("Member not found");

            if (book is IBorrowable borrowable)
            {
                book.CurrentBorrowerId = member.memberID;
                member.BorrowedBookIds.Add(isbn);
                
                await _bookRepo.UpdateItem(book);
                await _memberRepo.UpdateItem(member);
            }
            else
            {
                throw new InvalidOperationException("This book cannot be borrowed.");
            }
        }

        public async Task ReturnBook(string isbn, string memberId)
        {
            var book = await _bookRepo.FindById(isbn);
            var member = await _memberRepo.FindById(memberId);
            if (member == null) throw new ArgumentException("Member not found");

            if (book.CurrentBorrowerId != member.memberID)
            {
                throw new InvalidOperationException("Book is not currently borrowed by this member.");
            }

            if (book is IBorrowable borrowable)
            {
                borrowable.ReturnBook(member);
                await _bookRepo.UpdateItem(book);
                await _memberRepo.UpdateItem(member);
            }
            else
            {
                throw new InvalidOperationException("This book cannot be returned.");
            }
        }

        public async Task<List<Book>> GetBorrowedBooks()
        {
            var books = await _bookRepo.GetAll();
            return books.Where(b => b.IsCheckedOut).ToList();
        }

        public async Task<List<Book>> GetOverdueBooks()
        {
            var books = await _bookRepo.GetAll();
            return books.Where(b => 
                b.IsCheckedOut && 
                b.DueDate.HasValue && 
                b.DueDate.Value < DateTime.Now
            ).ToList();
        }
    }
}
