using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using BB.SmsQuiz.ApiModel.Authentication;

namespace BB.SmsQuiz.Api.IntegrationTests
{
    public class ApiClient
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseApiUri"]),
            };

            return client;
        }

        public static HttpClient GetAuthenticatedClient()
        {
            var client = GetClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GetAuthenticationToken());
            return client;
        }

        /// <summary>
        /// Authenticates and returns a live auth token from API.
        /// </summary>
        /// <returns>An authentication token</returns>
        private static string GetAuthenticationToken()
        {
            var result = GetClient().PostAsJsonAsync(
                "authentication",
                new PostAuthentication()
                {
                    Password = "admin",
                    Username = "admin"
                }).Result;

            return result.Content.ReadAsAsync<string>().Result;
        }
    }
}
