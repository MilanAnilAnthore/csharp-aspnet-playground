using LibraryManager.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace LibraryManager.Models.Books
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(PhysicalBook), typeDiscriminator: "physical")]
    [JsonDerivedType(typeof(EBook), typeDiscriminator: "ebook")]
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
