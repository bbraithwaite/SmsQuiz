using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Controllers.Authentication
{
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
        public HttpResponseMessage Post(LoginItem item)
        {
            User user = _userRepository.FindAll().SingleOrDefault(u => u.Username == item.Username);

            if (user != null)
            {
                if (user.Password.EncryptedValue.SequenceEqual(_encryptionService.Encrypt(item.Password)))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, base.TokenAuthentication.GetToken());
                }
            }

            return  new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}