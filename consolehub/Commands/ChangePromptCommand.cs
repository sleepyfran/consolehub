using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consolehub.Util;

namespace Consolehub.Commands
{
    class ChangePromptCommand : Command
    {
        public override string Name => "prompt";

        /// <summary>
        /// New prompt to be set.
        /// </summary>
        private string newPrompt;

        public ChangePromptCommand() { }

        public ChangePromptCommand(string[] args)
        {
            if (args.Length > 1)
            {
                var sanitizedPrompt = args[1].Replace(@"""", "");
                newPrompt = sanitizedPrompt;
            }
        }

        public override Command CreateCommand(string[] args, string[] flags)
        {
            return new ChangePromptCommand(args);
        }

        public override Task Execute()
        {
            if (newPrompt == null)
            {
                PrintHelp();
                return Task.FromResult(0);
            }
            else if (newPrompt.Equals("--reset"))
            {
                SettingsManager.Remove("prompt");
                UI.DefaultPrompt = "> ";
                return Task.FromResult(0);
            }

            SettingsManager.Set("prompt", newPrompt);
            UI.DefaultPrompt = newPrompt;
            return Task.FromResult(0);
        }

        public override void PrintHelp()
        {
            UI.WriteLineBlue("prompt [new prompt] - Sets a new prompt");
            UI.WriteLineBlue("prompt --reset - Resets the prompt to the default one");
        }
    }
}
