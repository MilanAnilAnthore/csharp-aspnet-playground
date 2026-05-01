using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.Abstractions;

namespace LibraryManager.Models
{
    public class Member
    {
        public Guid memberID;
        public string Name { get; set; } = "";

        public List<string> BorrowedBookIds = [];
    }
}
