using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;


namespace TodoCliApp.Commands
{
    internal class AddCommand
    {
        public Command addCommand { get; }

            public AddCommand()
            {
                // Defining the subcommand to add
                addCommand = new Command("add", "use to add tasks");

                //Defining the options for subcommand
                Option<string> PriorityOption = new Option<string>("--priority", "Use this to add urgency of task");
            }
        
    }
}
