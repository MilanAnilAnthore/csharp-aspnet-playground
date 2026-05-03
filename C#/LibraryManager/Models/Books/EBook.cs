using LibraryManager.Models.Members;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Models.Books
{
    public class EBook : Book
    {
        public decimal fileSize { get; set; }
        public string URL { get; set; } = "";
        public override string GetDetails()
        {
            return $"{GetBaseDetails()} - fileSize:{fileSize}, URL:{URL}";
        }
    }
}
