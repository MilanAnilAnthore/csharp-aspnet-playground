using LibraryManager.Models.Members;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Models.Books
{
    public class PhysicalBook : Book
    {
        public string ShelfLocation { get; set; } = "";
        public override string GetDetails()
        {
            return $"{GetBaseDetails()} - ShelfLocation:{ShelfLocation}";
        }
    }
}
