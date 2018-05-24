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
        /// Username in GitHub from whom we will get the repositories.
        /// </summary>
        private string username;

        /// <summary>
        /// Indicates whether we should ignore the private repositories or not.
        /// </summary>
        private bool ignorePrivateRepositories = false;

        public ReposCommand() { }

        public override Command CreateCommand(string[] args, string[] flags)
        {
            var command = new ReposCommand();

            // Attempt to get the available flags.
            if (flags.Length > 0)
            {
                bool ignorePrivates = flags
                                        .Where(arg => arg.Equals("--ignore-private"))
                                        .Count() > 0;

                command.ignorePrivateRepositories = ignorePrivates;
            }

            // Attempt to find some args.
            if (args.Length > 0)
            {
                command.username = args[0];
            }

            return command;
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

            if (ignorePrivateRepositories)
            {
                repositories = repositories.Where(repo => !repo.Private).ToList();
            }

            Console.WriteLine("Repositories count: {0}", repositories.Count);

            for (int i = 0; i < repositories.Count; i++)
            {
                var repository = repositories[i];

                UI.WriteBlue($"{i + 1}. {repository.FullName}: ");
                Console.WriteLine($"{repository.Description}");
            }
        }

        public override void PrintHelp()
        {
            UI.WriteLineBlue("repos [username] [options] - List all the repos of the specified username");
            UI.WriteLineBlue("OPTIONS");
            UI.WriteLineBlue("--ignore-private: Ignore user's private repositories");
        }
    }
}
