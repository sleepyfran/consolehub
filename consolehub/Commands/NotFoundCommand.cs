using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using consolehub.Util;

namespace consolehub.Commands
{
    class NotFoundCommand : Command
    {
        public override string Name => "";

        public override Command CreateCommand(string[] args)
        {
            return new NotFoundCommand();
        }

        public override Task Execute()
        {
            UI.WriteLineRed("Command not recognized. Use help to print all the available commands");
            return Task.FromResult(0);
        }

        public override void PrintHelp()
        {
            Console.WriteLine("");
        }
    }
}
