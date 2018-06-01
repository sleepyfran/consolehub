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

        /// <summary>
        /// Shows an error indicating that the username could not be found.
        /// </summary>
        /// <param name="username">Username that could not be found</param>
        private void showUsernameNotFoundError(string username)
        {
            Ui.WriteLineRed($"The username {username} doesn't exist");
        }

        /// <summary>
        /// Shows an unknown exception in the screen.
        /// </summary>
        /// <param name="error">Exception to print</param>
        private void showException(Exception error)
        {
            Ui.WriteLineRed(error.Message);
        }

        /// <summary>
        /// Fetches the repositories of the current logged in user.
        /// </summary>
        /// <returns>Repositories of the current user</returns>
        private async Task<IReadOnlyList<Repository>> getUserRepositories()
        {
            try
            {
                var repositories = await GHClient.client.Repository.GetAllForCurrent();
                return repositories;
            }
            catch (ApiException error)
            {
                showException(error);
            }

            return new List<Repository>();
        }

        /// <summary>
        /// Fetches the repositories of the specified user.
        /// </summary>
        /// <returns>Repositories of the spcecified user</returns>
        private async Task<IReadOnlyList<Repository>> getRepositoriesOf(string username)
        {
            try
            {
                var repositories = await GHClient.client.Repository.GetAllForUser(username);
                return repositories;
            }
            catch (ApiException error)
            {
                showUsernameNotFoundError(username);
            }

            return new List<Repository>();
        }

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
                repositories = await getUserRepositories();
            }
            else
            {
                Console.WriteLine("Getting repos from {0}...", username);
                repositories = await getRepositoriesOf(username);
            }           

            if (ignorePrivateRepositories)
            {
                repositories = repositories.Where(repo => !repo.Private).ToList();
            }

            Console.WriteLine("Repositories count: {0}", repositories.Count);

            for (int i = 0; i < repositories.Count; i++)
            {
                var repository = repositories[i];

                Console.Write($"{i + 1}. ");
                Ui.WriteCyan($"{repository.FullName}");
                Console.Write($" - ");
                Ui.WriteBlue(repository.Description);

                Console.WriteLine();
            }
        }

        public override void PrintHelp()
        {
            Ui.WriteLineBlue("repos [username] [options] - List all the repos of the specified username");
            Ui.WriteLineBlue("OPTIONS");
            Ui.WriteLineBlue("--ignore-private: Ignore user's private repositories");
        }
    }
}
