using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolehub.Util
{
    class GHClient
    {
        /// <summary>
        /// Default GitHub client used by the app.
        /// </summary>
        static public GitHubClient client = new GitHubClient(new ProductHeaderValue("consolehub"));

        /// <summary>
        /// Generates an OauthLoginRequest and uses it to obtain the login URL.
        /// </summary>
        static public Uri GetLoginUrl()
        {
            var request = new OauthLoginRequest(Credentials.API_KEY)
            {
                Scopes = { "user", "repo", "notifications" }
            };

            return client.Oauth.GetGitHubLoginUrl(request);
        }

        /// <summary>
        /// Generates an access token give a response URL from the user log in.
        /// </summary>
        /// <param name="responseUrl">User log in response URL</param>
        /// <returns>String containing the access token of the user</returns>
        static public async Task<string> GetAccessToken(string responseUrl)
        {
            var code = getCodeFromUrl(responseUrl);
            var request = new OauthTokenRequest(Credentials.API_KEY, Credentials.API_SECRET, code);
            var token = await client.Oauth.CreateAccessToken(request);

            return token.AccessToken;
        }

        /// <summary>
        /// Returns the access code from the given URL.
        /// </summary>
        /// <param name="responseUrl">User log in response URL</param>
        /// <returns>Code from the URL</returns>
        static private string getCodeFromUrl(string responseUrl)
        {
            var queries = new Uri(responseUrl).Query;
            var queryDictionary = System.Web.HttpUtility.ParseQueryString(queries);

            return queryDictionary["code"];
        }
    }
}
