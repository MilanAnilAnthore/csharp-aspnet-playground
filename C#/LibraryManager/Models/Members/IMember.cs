using LibraryManager.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Models.Members
{
    public interface IMember : IIdentifiable
    {
        public Guid memberID { get; set; }

         public string Name { get; set; }

        public List<string> BorrowedBookIds { get; set; }
    }
}
