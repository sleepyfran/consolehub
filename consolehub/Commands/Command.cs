using Consolehub.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehub.Commands
{
    public abstract class Command
    {
        /// <summary>
        /// Indicates the text that matches this command. Ex: login, repository or user.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// General description of the command. What it does, how it does it, etc.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Options (or flags) of the command. Example: --reset in ChangePromptCommand.
        /// </summary>
        public abstract string[] Options { get; }

        /// <summary>
        /// Checks whether the user is logged in and has access to the current command. Called from a
        /// Command subclass.
        /// </summary>
        /// <returns>True if user is logged in, False if not</returns>
        protected bool IsUserLoggedIn()
        {
            if (SettingsManager.Exists("access_token")) return true;

            Ui.WriteLineRed("You need to log in before doing this.");
            return false;
        }

        /// <summary>
        /// Executes the current command.
        /// </summary>
        public abstract Task Execute();

        /// <summary>
        /// Creates a command from the given args.
        /// </summary>
        /// <param name="args">Arguments passed to the command</param>
        /// <param name="flags">Flags passed to the command</param>
        public abstract Command CreateCommand(string[] args, string[] flags);
    }
}
