using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.Abstractions;

namespace LibraryManager.Models
{
    public class PhysicalBook : Book
    {
        public string shelfLocation = "";
        public override string GetDetails()
        {
            return $"{GetBaseDetails()} - ShelfLocation:{shelfLocation}";
        }
    }
}
