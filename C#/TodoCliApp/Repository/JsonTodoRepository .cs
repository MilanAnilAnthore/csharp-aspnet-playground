using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using TodoCliApp.Models;
using TodoCliApp.Constants;

namespace TodoCliApp.Repository
{
    public class JsonTodoRepository : ITodoRepository
    {

        public async Task<List<Todo>> GetAllAsync()
        {
            var DataFilePath = ConstantData.DataFilePath;
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
            var DataFilePath = ConstantData.DataFilePath;
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            using FileStream createStream = File.Create(DataFilePath);
            await JsonSerializer.SerializeAsync(createStream, todoList);

        }
    }
}
