using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;
using TodoCliApp.Repository;

namespace TodoCliApp.Commands
{
    internal class DeleteCommand
    {
        private readonly IServiceRepository _service;
        public Command deleteCommand { get; }
        public DeleteCommand(IServiceRepository service)
        {
            _service = service;

            deleteCommand = new Command("delete", "Use to delete a command");

            Argument<string> idArgument = new Argument<string>("id");

            deleteCommand.Arguments.Add(idArgument);

            deleteCommand.SetAction(async parseResults =>
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
                    await _service.DeleteTodoAsync(idValue);
                    Console.WriteLine("Task deleted successfully!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

            });
        }
    }
}
