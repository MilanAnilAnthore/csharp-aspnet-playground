using System;
using System.Collections.Generic;
using System.Text;
using TodoCliApp.Models;

namespace TodoCliApp.Repository
{
    // This is the interface/contract to make jsonTodoRepository class
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task SaveAllAsync(List<Todo> todoList);
    }
}
