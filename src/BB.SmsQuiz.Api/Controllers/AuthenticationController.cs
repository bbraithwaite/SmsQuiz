// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.Authentication;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The authentication controller.
    /// </summary>
    [UnhandledException]
    public class AuthenticationController : BaseController
    {
        /// <summary>
        /// The _encryption service.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        /// The _user data mapper.
        /// </summary>
        private readonly IUserDataMapper _userDataMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="userDataMapper">
        /// The user Data Mapper.
        /// </param>
        /// <param name="encryptionService">
        /// The encryption service.
        /// </param>
        public AuthenticationController(
            IUserDataMapper userDataMapper, 
            IEncryptionService encryptionService)
        {
            _userDataMapper = userDataMapper;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// The post method.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        public HttpResponseMessage Post(PostAuthentication item)
        {
            var user = _userDataMapper.FindByUsername(item.Username);

            if (user != null)
            {
                if (user.Password.EncryptedValue.SequenceEqual(_encryptionService.Encrypt(item.Password)))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TokenAuthentication.Token);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}