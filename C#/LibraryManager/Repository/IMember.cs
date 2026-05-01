using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Repository
{
    public interface IMember
    {
        public Guid memberID { get; set; }
        public string Name { get; set; }

        public List<string> BorrowedBookIds { get; set; }
    }
}
