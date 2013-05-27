using System.Net.Http;
using System.Net.Http.Headers;

namespace BB.SmsQuiz.Web.Infrastructure
{
    public interface IBaseContext
    {
        IFormsAuthentication FormsAuthentication { get; }
        HttpClient Client { get; }
    }

    public class BaseContext : IBaseContext
    {
        private readonly HttpClient _client;

        private readonly IFormsAuthentication _formsAuthentication;

        public BaseContext(HttpClient client, IFormsAuthentication authentication)
        {
            _client = client;
            _formsAuthentication = authentication;
        }

        public IFormsAuthentication FormsAuthentication
        {
            get { return _formsAuthentication; }
        }

        public HttpClient Client {
            get
            {
                if (!string.IsNullOrEmpty(FormsAuthentication.GetAuthenticationToken()))
                {
                    _client.DefaultRequestHeaders.Authorization 
                        = new AuthenticationHeaderValue(FormsAuthentication.GetAuthenticationToken());
                }

                return _client;
            }
        }
    }
}