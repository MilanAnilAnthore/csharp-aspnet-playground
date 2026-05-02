using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;
using LibraryManager.Repository;

namespace LibraryManager.Abstractions
{
    public abstract class Book : IIdentifiable
    {
        public string Id => ISBN;
        public string title="";
        public string author = "";
        public string genre="";
        public string ISBN = "";
        public Boolean IsCheckedOut = false;
        public Guid? CurrentBorrowerId { get; set; }
        public DateTime? DueDate { get; set; }

        protected string GetBaseDetails()
        {
            return $"[{ISBN}] - {title} - Author: {author} - Genre: {genre}";
        }

        public abstract string GetDetails();
    }
}
