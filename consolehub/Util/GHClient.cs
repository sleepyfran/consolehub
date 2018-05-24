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
        static public GitHubClient client { get; } = 
            new GitHubClient(
                new ProductHeaderValue("consolehub"));

        /// <summary>
        /// Generates an OauthLoginRequest and uses it to obtain the login URL.
        /// </summary>
        static public Uri GetLoginUrl()
        {
            var request = new OauthLoginRequest(ApiKeys.API_KEY)
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
            var request = new OauthTokenRequest(ApiKeys.API_KEY, ApiKeys.API_SECRET, code);
            var token = await client.Oauth.CreateAccessToken(request);

            return token.AccessToken;
        }

        /// <summary>
        /// Sets the credentials in the client so we can start making requests.
        /// </summary>
        /// <param name="accessToken">String containing the access token of the user</param>
        static public void SetCredentials(string accessToken)
        {
            client.Credentials = new Octokit.Credentials(accessToken);
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
