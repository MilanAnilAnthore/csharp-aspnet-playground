using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TodoCliApp.Constants;
using TodoCliApp.Models;

namespace TodoCliApp.Repository
{
    public class JsonTodoRepository : ITodoRepository
    {

        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public async Task<List<Todo>> GetAllAsync()
        {
            var DataFilePath = ConstantData.DataFilePath;
            try
            {
                if (File.Exists(DataFilePath))
                {
                    using FileStream openStream = File.OpenRead(DataFilePath);
                    List<Todo> list = await JsonSerializer.DeserializeAsync<List<Todo>>(openStream, _options) ?? new List<Todo>();
                    return list;
                }
                else
                {
                    return new List<Todo>();
                }
            }
            catch
            {
                return new List<Todo>();
            }
            
        }

        public async Task SaveAllAsync(List<Todo> todoList)
        {
            var DataFilePath = ConstantData.DataFilePath;
            using FileStream createStream = File.Create(DataFilePath);
            await JsonSerializer.SerializeAsync(createStream, todoList, _options);

        }
    }
}
