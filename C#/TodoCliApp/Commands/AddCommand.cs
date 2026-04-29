using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using TodoCliApp.Services;
using TodoCliApp.Repository;
using TodoCliApp.Models;

namespace TodoCliApp.Commands
{
    internal class AddCommand
    {
        // exposing just the getter so no access to change the command
        public Command addCommand { get; }
        private readonly TodoService _service;

        public AddCommand(TodoService service)
        {
            _service = service;

            // Defining the subcommand to add
            addCommand = new Command("add", "use to add tasks");

                //Defining the options and arguments for subcommand
                Argument<string> titleArgument = new Argument<string>("title");
                Option<string> PriorityOption = new Option<string>("--priority", "-p")
                    {
                    Description = "Use this to add urgency of task"
                     };
                Option<DateTime> dueDateOption = new Option<DateTime>("--due", "-d")
                {
                Description = "Use this to add due of task"
                };

            // adding the options and arguments for subcommand
            addCommand.Add(titleArgument);
            addCommand.Options.Add(PriorityOption);
            addCommand.Options.Add(dueDateOption);

            // retrieving the value from clis
            addCommand.SetAction(async parseResult =>
            {
                // assigning the retrieved value to appropriate variables
                var titleValue = parseResult.GetValue(titleArgument);
                var priorityValue = parseResult.GetValue(PriorityOption);
                var dateTimeValue = parseResult.GetValue(dueDateOption);

                if (string.IsNullOrWhiteSpace(titleValue))
                {
                    Console.WriteLine("Error: Title cannot be empty.");
                    return;
                }

                // Default to a priority
                Priority priorityEnum = Priority.Medium;

                // If user provided a priority then attempt to parse it
                if (!string.IsNullOrWhiteSpace(priorityValue))
                {
                    if (!Enum.TryParse(priorityValue, true, out priorityEnum))
                    {
                        Console.WriteLine($"Error: '{priorityValue}' is not a valid priority.");
                        return;
                    }
                }

                try
                {
                    await _service.AddTodoAsync(titleValue, priorityEnum, dateTimeValue);
                    Console.WriteLine($"Task '{titleValue}' added successfully!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            });

        }

    }
}
