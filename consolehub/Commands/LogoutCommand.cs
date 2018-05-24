﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consolehub.Util;

namespace consolehub.Commands
{
    class LogoutCommand : Command
    {
        public override string Name => "logout";

        public override Command CreateCommand(string[] args)
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

        public override void PrintHelp()
        {
            Console.WriteLine("logout - Logs out from the current account");
        }
    }
}