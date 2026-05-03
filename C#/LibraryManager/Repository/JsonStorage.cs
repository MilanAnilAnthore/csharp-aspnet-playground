using LibraryManager.Models.Books;
using LibraryManager.Models.Members;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManager.Repository
{
    public class JsonStorage
    {
        private readonly JsonSerializerOptions _options = new()
        {
            // pretty printing
            WriteIndented = true,
            // store enum as string rather than number
            Converters = { new JsonStringEnumConverter() }
        };

        private readonly string FilePathBook = "book.json";
        private readonly string FilePathMember = "member.json";


        public async Task<List<Book>> GetAllBooks()
        {
            try
            {
                if (File.Exists(FilePathBook))
                {
                    using FileStream openStream = File.OpenRead(FilePathBook);
                    List<Book> list = await JsonSerializer.DeserializeAsync<List<Book>>(openStream, _options) ?? new List<Book>();
                    return list;
                }
                else
                {
                    return new List<Book>();
                }
            }
            catch (JsonException ex)
            {
                {
                    Console.Error.WriteLine($"Warning: Could not parse {FilePathBook}: {ex.Message}");
                    return new List<Book>();
                }

            }
        }

        public async Task SaveAllBooks(List<Book> books)
        {
            await using FileStream createStream = File.Create(FilePathBook);
            await JsonSerializer.SerializeAsync(createStream, books, _options);
        }


        public async Task<List<Member>> GetAllMember()
        {
            try
            {
                if (File.Exists(FilePathMember))
                {
                    using FileStream openStream = File.OpenRead(FilePathMember);
                    List<Member> list = await JsonSerializer.DeserializeAsync<List<Member>>(openStream, _options) ?? new List<Member>();
                    return list;
                }
                else
                {
                    return new List<Member>();
                }
            }
            catch (JsonException ex)
            {
                {
                    {
                        Console.Error.WriteLine($"Warning: Could not parse {FilePathMember}: {ex.Message}");
                        return new List<Member>();
                    }
                }

            }
        }

        public async Task SaveAllMember(List<Member> members)
        {
            await using FileStream createStream = File.Create(FilePathMember);
            await JsonSerializer.SerializeAsync(createStream, members, _options);
        }
    }
}
