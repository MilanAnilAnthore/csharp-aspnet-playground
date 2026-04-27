using System;
using System.Collections.Generic;
using System.Text;
using TodoCliApp.Models;

namespace TodoCliApp.Repository
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task SaveAllAsync(List<Todo> todoList);
    }
}
