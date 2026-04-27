using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using TodoCliApp.Models;

namespace TodoCliApp.Repository
{
    public class JsonTodoRepository : ITodoRepository
    {
        private const string DataFilePath = "data.json";

        public async Task<List<Todo>> GetAllAsync()
        {
            if (File.Exists(DataFilePath))
            {
                using FileStream openStream = File.OpenRead(DataFilePath);
                List<Todo> list = await JsonSerializer.DeserializeAsync<List<Todo>>(openStream) ?? new List<Todo>();
                return list;
            }
            else
            {
                return new List<Todo>();
            }
        }

        public async Task SaveAllAsync(List<Todo> todoList)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            using FileStream createStream = File.Create("data.json");
            await JsonSerializer.SerializeAsync(createStream, todoList);

        }
    }
}
