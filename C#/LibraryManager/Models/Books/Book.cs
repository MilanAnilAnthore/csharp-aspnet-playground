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
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Genre { get; set; } = "";
        public string ISBN { get; set; } = "";
        public bool IsCheckedOut { get; set; } = false;
        public Guid? CurrentBorrowerId { get; set; }
        public DateTime? DueDate { get; set; }

        protected string GetBaseDetails()
        {
            return $"[{ISBN}] - {Title} - Author: {Author} - Genre: {Genre}";
        }

        public abstract string GetDetails();
    }
}
