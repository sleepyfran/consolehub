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
                new Login(),
                new Repos()
            };
            var parser = new CommandParser(availableCommands);

            ConsoleHub(parser).GetAwaiter().GetResult();
        }

        static async Task ConsoleHub(CommandParser parser)
        {
            if (!SettingsManager.Exists("access_token"))
            {
                Console.WriteLine("Seems like you haven't logged in yet. Let's do it!");
                var loginCmd = new Login();
                await loginCmd.Execute();
            }

            Console.WriteLine("You're logged in!");
            string[] input = { "" };

            while (!input[0].Equals("exit"))
            {
                Console.Write("> ");
                input = Console.ReadLine().Split();

                var cmd = parser.ParseCommand(input);
                await cmd.Execute();
            }
        }
    }
}
