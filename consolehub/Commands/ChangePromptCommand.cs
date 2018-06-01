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

        public override string Description => "prompt [new prompt] [options]: Sets a new prompt to the program";

        public override string[] Options => new[]
        {
            "--reset: Resets the prompt to the default one",
        };

        /// <summary>
        /// New prompt to be set.
        /// </summary>
        private string newPrompt;

        /// <summary>
        /// Indicates whether the prompt should or should not be reset.
        /// </summary>
        private bool resetPrompt = false;

        public ChangePromptCommand() { }

        public override Command CreateCommand(string[] args, string[] flags)
        {
            var command = new ChangePromptCommand();

            // Handle flags (if any).
            if (flags.Length > 0)
            {
                command.resetPrompt = flags.Count(flag => flag == "--reset") > 0;
            }

            // Handle args (if any).
            if (args.Length > 0)
            {
                var sanitizedPrompt = args[0].Replace(@"""", "");
                command.newPrompt = sanitizedPrompt;
            }

            return command;
        }

        public override Task Execute()
        {
            if (resetPrompt)
            {
                SettingsManager.Remove("prompt");
                Ui.DefaultPrompt = "> ";
                return Task.FromResult(0);
            } else if (newPrompt == null)
            {
                return Task.FromResult(0);
            }

            SettingsManager.Set("prompt", newPrompt);
            Ui.DefaultPrompt = newPrompt;
            return Task.FromResult(0);
        }
    }
}
