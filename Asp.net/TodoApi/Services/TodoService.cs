using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Models;
using TodoApi.Constants;
using TodoApi.Repository;

namespace TodoApi.Services
{
    public class TodoService(ITodoRepository _repository) : IServiceRepository
    {

        // Use to add a Todo Task
        public async Task AddTodoAsync(string title, Priority priority, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            }
            else if (DateTime.Today > dueDate)
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

        // Use to get all the existing TodoTasks
        public async Task<List<Todo>> GetAllTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Use to get a single todo task
        public async Task<Todo> GetTodoById(string id)
        {
            if (Guid.TryParse(id, out Guid newID))
            {
                List<Todo> loadedData = await _repository.GetAllAsync();

                var result = loadedData.FirstOrDefault(a => a.Id == newID);

                if (result != null)
                {
                    return result;
                }
                else
                {
                    throw new ArgumentException("A task with this id does not exist", nameof(id));
                }
            }
            throw new ArgumentException("Id cannot be empty", nameof(id));
     
        }

        // Use to delete a single TodoTask using its id
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
    }
}
