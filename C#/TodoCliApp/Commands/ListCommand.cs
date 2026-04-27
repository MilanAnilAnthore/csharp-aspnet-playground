using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;
using TodoCliApp.Models;
using TodoCliApp.Services;

namespace TodoCliApp.Commands
{
    internal class ListCommand
    {
        private readonly TodoService _service;

        public Command listCommand { get; }

        public ListCommand(TodoService service)
        {
            _service = service;

            listCommand = new Command("list", "use to list tasks");

            Option<string> statusOption = new Option<string>("--status", "-s")
            {
                Description = "Use this option to filter using status of tasks"
            };

            listCommand.Options.Add(statusOption);

            listCommand.SetAction(async parseResult =>
            {
                var statusValue = parseResult.GetValue(statusOption);

                if (!string.IsNullOrWhiteSpace(statusValue))
                {
                    // Try to parse the status or return an error if invalid
                    if (!Enum.TryParse(statusValue, true, out TodoStatus statusEnum))
                    {
                        Console.WriteLine($"Error: '{statusValue}' is not a valid status.");
                        return;
                    }

                    var todoList = await _service.GetByStatusAsync(statusEnum);
                    if(todoList.Count != 0)
                    {
                        foreach (var ListItem in todoList)
                        {
                            Console.WriteLine(ListItem);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No todos found with status '{statusEnum}'");
                        return;
                    }
                }
                else
                {
                    var todoList = await _service.GetAllTodosAsync();  
                    foreach(var ListItem in todoList)
                    {
                        Console.WriteLine(ListItem);
                    }
                }
            });
        }
    }
}
