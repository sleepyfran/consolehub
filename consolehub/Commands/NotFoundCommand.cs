using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consolehub.Util;

namespace Consolehub.Commands
{
    class NotFoundCommand : Command
    {
        public override string Name => "";

        public override Command CreateCommand(string[] args, string[] flags)
        {
            return new NotFoundCommand();
        }

        public override Task Execute()
        {
            Ui.WriteLineRed("Command not recognized. Use help to print all the available commands");
            return Task.FromResult(0);
        }

        public override void PrintHelp()
        {
            Console.WriteLine("");
        }
    }
}
