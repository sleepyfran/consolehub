using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consolehub.Commands;
using Consolehub.Util;
using System.Text.RegularExpressions;

namespace Consolehub
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize app.
            SettingsManager.Init();
            ICommand[] availableCommands =
            {
                new LoginCommand(),
                new LogoutCommand(),
                new ReposCommand(),
                new ExitCommand(),
                new ClearCommand(),
                new ChangePromptCommand(),
                new IssuesCommand(),
            };
            var parser = new CommandParser(availableCommands);

            UI.PrintMainTitle();

            ConsoleHub(parser).GetAwaiter().GetResult();
        }

        static async Task ConsoleHub(CommandParser parser)
        {
            // Initialize the prompt from the settings.
            if (SettingsManager.Exists("prompt"))
            {
                var prompt = SettingsManager.Get("prompt");
                UI.DefaultPrompt = prompt;
            }

            if (!SettingsManager.Exists("access_token"))
            {
                Console.WriteLine("Seems like you haven't logged in yet. Let's do it!");
                var loginCmd = new LoginCommand();
                await loginCmd.Execute();
            }

            // Initialize the credentials of the user with the access token.
            var accessToken = SettingsManager.Get("access_token");
            GHClient.SetCredentials(accessToken);

            // Get the current logged in user to show the data.
            var currentUser = await GHClient.client.User.Current();
            UI.WriteLineGreen($"You're logged in as {currentUser.Name} ({currentUser.Email})");
            string[] dividedCommand;

            while (true)
            {
                UI.WritePrompt();

                var input = Console.ReadLine();
                dividedCommand = parser.SplitInput(input);

                var cmd = parser.ParseCommand(dividedCommand);
                UI.NewLine();
                await cmd.Execute();
            }
        }
    }
}
