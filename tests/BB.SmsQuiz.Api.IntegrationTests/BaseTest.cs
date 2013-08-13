// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseTest.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using BB.SmsQuiz.ApiModel.Users;

namespace BB.SmsQuiz.Api.IntegrationTests
{
    /// <summary>
    /// The base test class.
    /// </summary>
    public class BaseTest
    {
        /// <summary>
        /// The _client.
        /// </summary>
        private HttpClient _client;

        /// <summary>
        /// Gets the client.
        /// </summary>
        public HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = ApiClient.GetAuthenticatedClient();
                }

                return _client;
            }
        }

        /// <summary>
        /// The get user.
        /// </summary>
        /// <returns>
        /// The <see cref="GetUser"/>.
        /// </returns>
        protected GetUser GetUser()
        {
            return Client
                .GetAsync(Resources.Users)
                .Result.Content.ReadAsAsync<IEnumerable<GetUser>>()
                .Result.First();
        }
    }
}