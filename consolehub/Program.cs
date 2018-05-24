using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consolehub.Commands;
using consolehub.Util;

namespace consolehub
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize app.
            SettingsManager.Init();
            Command[] availableCommands =
            {
                new LoginCommand(),
                new ReposCommand(),
                new ExitCommand(),
            };
            var parser = new CommandParser(availableCommands);

            ConsoleHub(parser).GetAwaiter().GetResult();
        }

        static async Task ConsoleHub(CommandParser parser)
        {
            if (!SettingsManager.Exists("access_token"))
            {
                Console.WriteLine("Seems like you haven't logged in yet. Let's do it!");
                var loginCmd = new LoginCommand();
                await loginCmd.Execute();
            }

            // Initialize the credentials of the user with the access token.
            var accessToken = SettingsManager.Get("access_token");
            GHClient.SetCredentials(accessToken);

            Console.WriteLine("You're logged in!");
            string[] input;

            while (true)
            {
                Console.Write("> ");
                input = Console.ReadLine().Split();

                var cmd = parser.ParseCommand(input);
                await cmd.Execute();
            }
        }
    }
}
