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

        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task AddTodoAsync(string title, Priority priority, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            }else if (DateTime.Now > dueDate)
            {
                throw new ArgumentException("The due date has already expired.", nameof(dueDate));
            }
            Guid newGuid = Guid.NewGuid();

            Todo newTodo = new()
            {
                Id = newGuid,
                Title = title,
                DueDate = dueDate,
                Priority = priority,
                Status = TodoStatus.Pending,
                CreatedAt = DateTime.Now
            };

            List<Todo> loadedData = await _repository.GetAllAsync();
            loadedData.Add(newTodo);
            await _repository.SaveAllAsync(loadedData);
        }

        public async Task<List<Todo>> GetAllTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CompleteTodoAsync(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }

            List<Todo> loadedData = await _repository.GetAllAsync();

            var result = loadedData.FirstOrDefault(a=> a.Id == id);

            if(result != null)
            {
                result.Status = TodoStatus.Completed;
                await _repository.SaveAllAsync(loadedData);
            }
            else
            {
                throw new ArgumentException("A task with this id does not exist", nameof (id));
            }
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }

            List<Todo> loadedData = await _repository.GetAllAsync();

            var result = loadedData.FirstOrDefault(a => a.Id == id);

            if (result != null)
            {
                loadedData.Remove(result);
                await _repository.SaveAllAsync(loadedData);
            }
            else
            {
                throw new ArgumentException("A task with this id does not exist", nameof(id));
            }
        }

        public async Task<List<Todo>> GetByStatusAsync(TodoStatus status)
        {
            if (status == TodoStatus.Completed || status == TodoStatus.Pending)
            {
                List<Todo> loadedData = await _repository.GetAllAsync();
                var filteredData = loadedData.Where(a => a.Status == status);
                if (filteredData.Any())
                {
                    return filteredData.ToList();
                }
                else
                {
                    throw new ArgumentException("There is no data with status", nameof(status));
                }
            }

            throw new ArgumentException("Invalid status provided", nameof(status));
        }
    }
}
