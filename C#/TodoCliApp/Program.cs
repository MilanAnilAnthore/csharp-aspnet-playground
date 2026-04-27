using System.CommandLine;
using System.CommandLine.Parsing;
using TodoCliApp.Commands;
using TodoCliApp.Repository;
using TodoCliApp.Services;

//dependencys
var jsonRepo = new JsonTodoRepository();
var services = new TodoService(jsonRepo);
var addCommand = new AddCommand(services);

// rootCommand
var rootCommand = new RootCommand("Sample command-line TO-Do app");

//SubCommands


rootCommand.Subcommands.Add(addCommand.addCommand);

return rootCommand.Parse(args).Invoke();
