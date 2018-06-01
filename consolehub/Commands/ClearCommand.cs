using Consolehub.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehub.Commands
{
    class ClearCommand : Command
    {
        public override string Name => "clear";

        public override string Description => "clear: Clears the console screen";

        public override string[] Options => new string[0];

        public override Command CreateCommand(string[] args, string[] flags)
        {
            return new ClearCommand();
        }

        public override Task Execute()
        {
            Console.Clear();
            return Task.FromResult(0);
        }
    }
}
