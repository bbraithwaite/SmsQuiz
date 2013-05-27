using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.Authentication;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Controllers
{
    [UnhandledException]
    public class AuthenticationController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;

        public AuthenticationController(IUserRepository userRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        // POST authenticationn
        public HttpResponseMessage Post(PostAuthentication item)
        {
            var user = _userRepository.Find("@username=username", new {Username = item.Username}).FirstOrDefault();
 
            if (user != null)
            {
                if (user.Password.EncryptedValue.SequenceEqual(_encryptionService.Encrypt(item.Password)))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, base.TokenAuthentication.Token);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}