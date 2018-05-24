using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehub.Commands
{
    class ExitCommand : Command
    {
        public override string Name => "exit";

        public override Command CreateCommand(string[] args)
        {
            return new ExitCommand();
        }

        public override Task Execute()
        {
            Environment.Exit(0);
            return Task.FromResult(0);
        }

        public override void PrintHelp()
        {
            Console.WriteLine("exit - Pretty self-explanatory, don't you think?");
        }
    }
}
