using Consolehub.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehub.Commands
{
    class HelpCommand : Command
    {
        public override string Name => "help";

        /// <summary>
        /// List of available commands in the program.
        /// </summary>
        private IEnumerable<Command> availableCommands;

        public HelpCommand(IEnumerable<Command> availableCommands)
        {
            this.availableCommands = availableCommands;
        }

        public override Command CreateCommand(string[] args, string[] flags)
        {
            throw new NotImplementedException();
        }

        public override Task Execute()
        {
            foreach (var command in availableCommands)
            {
                command.PrintHelp();
                Ui.NewLine();
            }

            return Task.FromResult(0);
        }

        public override void PrintHelp()
        {
            throw new NotImplementedException();
        }
    }
}
