using LibraryManager.Abstractions;
using LibraryManager.Models;
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
    }
}
