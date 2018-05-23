using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehub.Commands
{
    public abstract class Command
    {
        /// <summary>
        /// Indicates the text that matches this command. Ex: login, repository or user.
        /// </summary>
        public String matchExpression { get; }

        public Command(String matchExpression)
        {
            this.matchExpression = matchExpression;
        }

        /// <summary>
        /// Executes the current command.
        /// </summary>
        public abstract Task Execute();

        /// <summary>
        /// Prints the command's help section.
        /// </summary>
        public abstract void PrintHelp();
    }
}
