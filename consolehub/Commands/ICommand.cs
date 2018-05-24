using Consolehub.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehub.Commands
{
    public abstract class ICommand
    {
        /// <summary>
        /// Indicates the text that matches this command. Ex: login, repository or user.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Checks whether the user is logged in and has access to the current command. Called from a
        /// Command subclass.
        /// </summary>
        /// <returns>True if user is logged in, False if not</returns>
        protected bool checkUserIsLoggedIn()
        {
            if (!SettingsManager.Exists("access_token"))
            {
                UI.WriteLineRed("You need to log in before doing this.");
                return false;
            }

            return true;
        }

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
        /// <param name="args">Arguments passed to the command</param>
        /// <param name="flags">Flags passed to the command</param>
        public abstract ICommand CreateCommand(string[] args, string[] flags);
    }
}
