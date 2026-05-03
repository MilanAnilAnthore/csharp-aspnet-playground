using System;
using System.Collections.Generic;
using System.Text;


namespace LibraryManager.Models.Members
{
    public class Member : IMember
    {

        public Guid memberID { get; set; }
        public string Id => memberID.ToString();
        public string Name { get; set; } = "";

        public List<string> BorrowedBookIds { get; set; } = [];
    }
}
