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
            Console.WriteLine("Successfully saved");
        }

        // Use to get all the existing TodoTasks
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

        // Use to get Todo with a specific task type(Either pending or completed)
        public async Task<List<Todo>> GetByStatusAsync(TodoStatus status)
        {
                List<Todo> loadedData = await _repository.GetAllAsync();
                var filteredData = loadedData.Where(a => a.Status == status);
                return filteredData.ToList();
        }


        // Sort the tasks in descending order
        public async Task<List<Todo>> GetSortedByPriorityAsync()
        {
            List<Todo> loadedData = await _repository.GetAllAsync();

            var sortedDescending = loadedData.OrderByDescending(a => a.Priority);

            return sortedDescending.ToList();
        }

        // Sort the tasks using the due date ascending order
        public async Task<List<Todo>> GetSortedByDueDateAsync()
        {
            List<Todo> loadedData = await _repository.GetAllAsync();

            var sortByDue = loadedData.OrderBy(a => a.DueDate);
            return sortByDue.ToList();
        }

        // Sort the tasks by status and then filter by highest priority
        public async Task<List<Todo>> GetByStatusSortedAsync(TodoStatus status)
        {
            var filterByStatus = await GetByStatusAsync(status);

            var sortedTasks = filterByStatus.OrderByDescending(t => t.Priority);

            return sortedTasks.ToList();
        }
    }
}
