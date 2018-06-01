using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Consolehub.Commands;

namespace Consolehub.Util
{
    class CommandParser
    {
        private IEnumerable<Command> availableCommands;

        public CommandParser(IEnumerable<Command> commands)
        {
            this.availableCommands = commands;
        }

        /// <summary>
        /// Divides the input by whitespaces ignoring those inside double quotes.
        /// </summary>
        /// <param name="input">Raw input taken from the console</param>
        /// <returns>Array of strings containing each token (usually [command] [args])</returns>
        internal string[] SplitInput(string input)
        {
            Regex whitespacesRegex = new Regex(@"[ ](?=(?:[^""]*""[^""]*"")*[^""]*$)");
            return whitespacesRegex.Split(input);
        }

        /// <summary>
        /// Finds and creates a command from a given set of tokens.
        /// </summary>
        /// <param name="args">Array of strings with the tokens</param>
        /// <returns>A Command interface subclass that matches the given command</returns>
        internal Command ParseCommand(string[] args)
        {
            var commandName = args[0];

            if (commandName == "help")
            {
                return new HelpCommand(availableCommands);
            }

            var command = FindCommand(commandName);

            if (command == null)
            {
                return new NotFoundCommand();
            }
            
            // Get all flags starting with --.
            string[] flags = args
                                .Where(arg => arg.StartsWith("--"))
                                .ToArray();

            if (flags.Contains("--help"))
            {
                command.PrintHelp();
                return null;
            }

            // The rest of them, skipping the first (command name) should be the arguments.
            string[] commandArgs = args
                                    .Skip(1)
                                    .Where(arg => !arg.StartsWith("--"))
                                    .ToArray();

            return command.CreateCommand(commandArgs, flags);
        }

        private Command FindCommand(String name)
        {
            return availableCommands
                   .FirstOrDefault(cmd => cmd.Name.Equals(name));
        }
    }
}
