using Consolehub.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consolehub.Commands
{
    class ExitCommand : Command
    {
        public override string Name => "exit";

        public override string Description => "exit: What do you think it does?";

        public override string[] Options => new string[0];

        public override Command CreateCommand(string[] args, string[] flags)
        {
            return new ExitCommand();
        }

        public override Task Execute()
        {
            Environment.Exit(0);
            return Task.FromResult(0);
        }
    }
}
