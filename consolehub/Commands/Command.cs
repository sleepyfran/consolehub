using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehub.Commands
{
    public abstract class Command
    {
        /// <summary>
        /// Indicates the text that matches this command. Ex: login, repository or user.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Executes the current command.
        /// </summary>
        public abstract Task Execute();

        /// <summary>
        /// Prints the command's help section.
        /// </summary>
        public abstract void PrintHelp();

        /// <summary>
        /// Creates a command from the given args.
        /// </summary>
        /// <param name="args"></param>
        public abstract Command CreateCommand(string[] args);
    }
}
