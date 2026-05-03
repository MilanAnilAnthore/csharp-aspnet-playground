using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using TodoCliApp.Repository;

namespace TodoCliApp.Commands
{
    internal class DoneCommand
    {
        private readonly IServiceRepository _service;
        public Command doneCommand { get; }
        public DoneCommand(IServiceRepository service)
        {
            _service = service;

            doneCommand = new Command("done", "Use to assign a task as completed");

            Argument<string> idArgument = new Argument<string>("id");

            doneCommand.Arguments.Add(idArgument);

            doneCommand.SetAction(async parseResults =>
            {
                var idString = parseResults.GetValue(idArgument);

                // Safely try to parse the string into a Guid
                if (!Guid.TryParse(idString, out Guid idValue))
                {
                    Console.WriteLine($"Error: '{idString}' is not a valid ID format. A valid ID looks like: 12345678-1234-1234-1234-123456789012");
                    return;
                }

                try
                {
                    await _service.CompleteTodoAsync(idValue);
                    Console.WriteLine("Task completed successfully!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

            });
        }
    }
}
