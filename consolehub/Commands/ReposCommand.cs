using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Consolehub.Util;

namespace Consolehub.Commands
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

        public override async Task Execute()
        {
            if (!checkUserIsLoggedIn())
            {
                return;
            }

            IReadOnlyList<Repository> repositories;

            if (username == null)
            {
                Console.WriteLine("Getting your repos...");
                repositories = await GHClient.client.Repository.GetAllForCurrent();
            }
            else
            {
                Console.WriteLine("Getting repos from {0}...", username);
                repositories = await GHClient.client.Repository.GetAllForUser(username);
            }

            Console.WriteLine("Repositories count: {0}", repositories.Count);

            for (int i = 0; i < repositories.Count; i++)
            {
                var repository = repositories[i];

                Console.WriteLine("{0}. {1} - {2}", i, repository.FullName, repository.Description);
            }
        }

        public override void PrintHelp()
        {
            Console.WriteLine("repos [username] - List all the repos of the specified username");
        }
    }
}
