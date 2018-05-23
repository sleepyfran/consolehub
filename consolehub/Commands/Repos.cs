using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehub.Commands
{
    class Repos : Command
    {
        public override string Name => "repos";

        /// <summary>
        /// Nombre del usuario en GitHub para obtener sus repositorios.
        /// </summary>
        private string username;

        public Repos() { }

        public Repos(string username)
        {
            this.username = username;
        }

        public override Command CreateCommand(string[] args)
        {
            if (args.Length > 1)
            {
                return new Repos(args[1]);
            }
            else
            {
                return new Repos();
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
