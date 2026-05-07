using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Models;

namespace TodoApi.Repository
{
    public interface IServiceRepository
    {
        public Task AddTodoAsync(string title, Priority priority, DateTime dueDate);
        public Task<List<Todo>> GetAllTodosAsync();
        public Task<Todo> GetTodoById(string id);
        public Task DeleteTodoAsync(Guid id);
    }
}
