using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.Abstractions;
using LibraryManager.Repository;

namespace LibraryManager.Models
{
    public class Member : IMember
    {
        public Guid memberID { get; set; }
        public string Name { get; set; } = "";

        public List<string> BorrowedBookIds { get; set; } = [];
    }
}
