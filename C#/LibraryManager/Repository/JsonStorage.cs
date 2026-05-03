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
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public async Task<List<T>> GetAllAsync<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using FileStream openStream = File.OpenRead(filePath);
                    List<T> list = await JsonSerializer.DeserializeAsync<List<T>>(openStream, _options) ?? new List<T>();
                    return list;
                }
                return new List<T>();
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"Warning: Could not parse {filePath}: {ex.Message}");
                return new List<T>();
            }
        }

        public async Task SaveAllAsync<T>(List<T> items, string filePath)
        {
            await using FileStream createStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(createStream, items, _options);
        }
    }
}
