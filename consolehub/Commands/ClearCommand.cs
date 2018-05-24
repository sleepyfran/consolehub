using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehub.Commands
{
    class ClearCommand : ICommand
    {
        public override string Name => "clear";

        public override ICommand CreateCommand(string[] args, string[] flags)
        {
            return new ClearCommand();
        }

        public override Task Execute()
        {
            Console.Clear();
            return Task.FromResult(0);
        }

        public override void PrintHelp()
        {
            Console.WriteLine("clear - Clears the current screen");
        }
    }
}
