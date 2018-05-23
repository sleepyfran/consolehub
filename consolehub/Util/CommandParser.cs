using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consolehub.Commands;

namespace consolehub.Util
{
    class CommandParser
    {
        private IEnumerable<Command> availableCommands;

        public CommandParser(IEnumerable<Command> commands)
        {
            this.availableCommands = commands;
        }

        internal Command ParseCommand(string[] args)
        {
            var commandName = args[0];
            var command = FindCommand(commandName);

            if (command == null)
            {
                return new NotFoundCommand();
            }

            return command;
        }

        private Command FindCommand(String name)
        {
            return availableCommands
                   .FirstOrDefault(cmd => cmd.Name.Equals(name));
        }
    }
}
