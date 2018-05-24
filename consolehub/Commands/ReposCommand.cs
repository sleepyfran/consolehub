using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehub.Commands
{
    class ReposCommand : Command
    {
        public override string Name => "repos";

        /// <summary>
        /// Nombre del usuario en GitHub para obtener sus repositorios.
        /// </summary>
        private string username;

        public ReposCommand() { }

        public ReposCommand(string username)
        {
            this.username = username;
        }

        public override Command CreateCommand(string[] args)
        {
            if (args.Length > 1)
            {
                return new ReposCommand(args[1]);
            }
            else
            {
                return new ReposCommand();
            }
        }

        public override Task Execute()
        {
            Console.WriteLine("Doing things");
            return Task.FromResult(0);
        }

        public override void PrintHelp()
        {
            Console.WriteLine("repos [username] - List all the repos of the specified username");
        }
    }
}
