using System.Net.Http;
using System.Net.Http.Headers;
using BB.SmsQuiz.Infrastructure.Mapping;

namespace BB.SmsQuiz.Web.Infrastructure
{
    public interface IBaseContext
    {
        IFormsAuthentication FormsAuthentication { get; }
        IMapper Mapper { get; }
        HttpClient Client { get; }
    }

    public class BaseContext : IBaseContext
    {
        private readonly HttpClient _client;

        private readonly IFormsAuthentication _formsAuthentication;

        private readonly IMapper _mapper;

        public BaseContext(HttpClient client, IFormsAuthentication authentication, IMapper mapper)
        {
            _client = client;
            _formsAuthentication = authentication;
            _mapper = mapper;
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

        public IMapper Mapper
        {
            get { return _mapper; }
        }
    }
}