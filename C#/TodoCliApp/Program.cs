using System.CommandLine;
using System.CommandLine.Parsing;
using TodoCliApp.Commands;
using TodoCliApp.Repository;
using TodoCliApp.Services;

//dependencys
var jsonRepo = new JsonTodoRepository();
var services = new TodoService(jsonRepo);
var addCommand = new AddCommand(services);
var listCommand = new ListCommand(services);
var doneCommand = new DoneCommand(services);
var deleteCommand = new DeleteCommand(services);

// rootCommand
var rootCommand = new RootCommand("Sample command-line TO-Do app");

//SubCommands
rootCommand.Subcommands.Add(addCommand.addCommand);
rootCommand.Subcommands.Add(listCommand.listCommand);
rootCommand.Subcommands.Add(doneCommand.doneCommand);
rootCommand.Subcommands.Add(deleteCommand.deleteCommand);


return rootCommand.Parse(args).Invoke();
