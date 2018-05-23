using System;
using consolehub.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace consolehub.Commands
{
    public class Login : Command
    {
        public Login(string matchExpression) : base(matchExpression) { }

        public override async Task Execute()
        {
            var loginUrl = GHClient.GetLoginUrl();

            Console.WriteLine("This app uses OAuth to log into your account.");
            Console.WriteLine("The app will attempt to open a window in 5 seconds, in case it doesn't work");
            Console.WriteLine("Open this URL:");
            Console.WriteLine(loginUrl);
            System.Threading.Thread.Sleep(5000);

            Process.Start(loginUrl.ToString());

            Console.WriteLine("...And once you finish, copy the URL and press any key. I'll be waiting.");
            Console.ReadKey();

            Console.WriteLine("Ready? Great! Copy the URL that you got over here.");
            var responseUrl = Console.ReadLine();

            if (String.IsNullOrEmpty(responseUrl) || 
                !responseUrl.StartsWith("http://localhost/oauth") ||
                !responseUrl.Contains("?code="))
            {
                Console.WriteLine("That doesn't seem like a valid URL >.<");
                Environment.Exit(0);
            }

            Console.WriteLine("Nice! Wait a second, we're getting your access token...");
            var accessToken = await GHClient.GetAccessToken(responseUrl);

            // Save this access token.
            SettingsManager.Set("access_token", accessToken);
        }

        public override void PrintHelp()
        {
            Console.WriteLine("login: Shows the login wizard to help you through the login process.");
        }
    }
}
