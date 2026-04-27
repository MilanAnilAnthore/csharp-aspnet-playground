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
        public Command addCommand { get; }
        private readonly TodoService _service;

        public AddCommand(TodoService service)
        {
            _service = service;

            // Defining the subcommand to add
            addCommand = new Command("add", "use to add tasks");

                //Defining the options for subcommand
                Argument<string> titleArgument = new Argument<string>("title");
                Option<string> PriorityOption = new Option<string>("--priority", "-p")
                    {
                    Description = "Use this to add urgency of task"
                     };
                Option<DateTime> dueDateOption = new Option<DateTime>("--due", "-d")
                {
                Description = "Use this to add due of task"
                };

            addCommand.Add(titleArgument);
            addCommand.Options.Add(PriorityOption);
            addCommand.Options.Add(dueDateOption);

            addCommand.SetAction(async parseResult =>
            {
                var titleValue = parseResult.GetValue(titleArgument);
                var priorityValue = parseResult.GetValue(PriorityOption);
                var dateTimeValue = parseResult.GetValue(dueDateOption);


                Console.WriteLine($"{titleValue}, {priorityValue}, {dateTimeValue}");

                if (Enum.TryParse(priorityValue, true, out Priority value) && !string.IsNullOrWhiteSpace(titleValue))
                {
                    await _service.AddTodoAsync(titleValue, value, dateTimeValue);
                }                
            });

        }

    }
}
