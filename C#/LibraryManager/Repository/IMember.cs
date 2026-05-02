using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.Repository;

namespace LibraryManager.Repository
{
    public interface IMember : IIdentifiable
    {
        public Guid memberID { get; set; }

         public string Name { get; set; }

        public List<string> BorrowedBookIds { get; set; }
    }
}
