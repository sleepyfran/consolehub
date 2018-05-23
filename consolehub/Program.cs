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
                new Login("login")
            };

            ConsoleHub(availableCommands).GetAwaiter().GetResult();
        }

        static async Task ConsoleHub(Command[] availableCommands)
        {
            if (!SettingsManager.Exists("access_token"))
            {
                Console.WriteLine("Seems like you haven't logged in yet. Let's do it!");
                await availableCommands
                    .Where(command => command.matchExpression == "login")
                    .First()
                    .Execute();
            }
            else
            {
                Console.WriteLine("You're logged in!");
                Console.ReadKey();
            }
        }
    }
}
