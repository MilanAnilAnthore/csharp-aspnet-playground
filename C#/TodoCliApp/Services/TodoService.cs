using System;
using System.Collections.Generic;
using System.Text;
using TodoCliApp.Constants;
using TodoCliApp.Models;
using TodoCliApp.Repository;

namespace TodoCliApp.Services
{
    internal class TodoService
    {

        public async Task AddTodoAsync(string title, Priority priority, DateTime dueDate)
        {
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Enter a title for yout todo");
                return;
            }else if (DateTime.Now > dueDate)
            {
                Console.WriteLine("The dueDate has already been expired");
                return;
            }
            Guid newGuid = Guid.NewGuid();

            Todo newTodo = new Todo()
            {
                Id = newGuid,
                Title = title,
                DueDate = dueDate,
                Priority = priority,
                Status = TodoStatus.Pending,
                CreatedAt = DateTime.Now
            };

            //await JsonTodoRepository.SaveAllAsync(newTodo);
        }

        public static List<Todo> LoadAccounts()
        {
            var DataFilePath = ConstantData.DataFilePath;

        }

    }
}
