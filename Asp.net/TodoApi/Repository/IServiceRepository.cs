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
        public Task CompleteTodoAsync(Guid id);
        public Task DeleteTodoAsync(Guid id);
        public Task<List<Todo>> GetByStatusAsync(TodoStatus status);
        public Task<List<Todo>> GetSortedByPriorityAsync();
        public Task<List<Todo>> GetSortedByDueDateAsync();
        public Task<List<Todo>> GetByStatusSortedAsync(TodoStatus status);
    }
}
