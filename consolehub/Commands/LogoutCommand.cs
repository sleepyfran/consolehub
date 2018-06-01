using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consolehub.Util;

namespace Consolehub.Commands
{
    class LogoutCommand : Command
    {
        public override string Name => "logout";

        public override string Description => "logout: Closes the current user session";

        public override string[] Options => new string[0];

        public override Command CreateCommand(string[] args, string[] flags)
        {
            return new LogoutCommand();
        }

        public override Task Execute()
        {
            var accessToken = SettingsManager.Get("access_token");
            SettingsManager.Remove("access_token");
            GHClient.RemoveCredentials(accessToken);

            Console.WriteLine("You're now logged out.");

            return Task.FromResult(0);
        }
    }
}
