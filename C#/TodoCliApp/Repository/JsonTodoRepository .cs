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
    // Assigning the ITodoRepository interface to the class
    public class JsonTodoRepository : ITodoRepository
    {
        // options for json serialization
        private readonly JsonSerializerOptions _options = new()
        {
            // pretty printing
            WriteIndented = true,
            // store enum as string rather than number
            Converters = { new JsonStringEnumConverter() }
        };


        // function to load data from JSON
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

        // function to save data to json
        public async Task SaveAllAsync(List<Todo> todoList)
        {
            var DataFilePath = ConstantData.DataFilePath;
            using FileStream createStream = File.Create(DataFilePath);
            await JsonSerializer.SerializeAsync(createStream, todoList, _options);

        }
    }
}
