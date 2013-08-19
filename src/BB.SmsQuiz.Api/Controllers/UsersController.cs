// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersController.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Infrastructure.UnitOfWork;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Controllers
{
    /// <summary>
    /// The users controller.
    /// </summary>
    [UnhandledException, TokenValidationAttribute]
    public class UsersController : BaseController
    {
        /// <summary>
        /// The _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The _encryption service.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        /// The _user data mapper.
        /// </summary>
        private readonly IUserDataMapper _userDataMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userDataMapper">
        /// The user repository.
        /// </param>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        /// <param name="encryptionService">
        /// The encryption service.
        /// </param>
        public UsersController(
            IUserDataMapper userDataMapper, 
            IMapper mapper,        
            IEncryptionService encryptionService)
        {
            _userDataMapper = userDataMapper;
            _mapper = mapper;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <remarks>
        /// GET user
        /// </remarks>
        public IEnumerable<GetUser> Get()
        {
            var users = _userDataMapper.FindAll();
            return _mapper.Map<IEnumerable<User>, IEnumerable<GetUser>>(users);
        }

        /// <summary>
        /// Gets a user by id
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="GetUser"/>.
        /// </returns>
        /// <exception cref="NotFoundException">
        /// Thrown for invalid user id.
        /// </exception>
        /// <remarks>
        /// GET users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        /// </remarks>
        public GetUser Get(Guid id)
        {
            var user = _userDataMapper.FindById(id);
            
            if (user == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<User, GetUser>(user);
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        /// <remarks>
        /// POST users
        /// </remarks>
        public HttpResponseMessage Post(PostUser item)
        {
            var user = new User()
                {
                    Username = item.Username, 
                    Password = EncryptedString.Create(item.Password, _encryptionService)
                };

            if (user.IsValid)
            {
                _userDataMapper.Insert(user);

                GetUser createdItem = _mapper.Map<User, GetUser>(user);
                return CreatedHttpResponse(createdItem.ID, createdItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, user.ValidationErrors);
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        /// <exception cref="NotFoundException">
        /// Thrown for invalid user id.
        /// </exception>
        /// <remarks>
        /// PUT users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        /// </remarks>
        public HttpResponseMessage Put(Guid id, PutUser item)
        {
            User user = _userDataMapper.FindById(id);

            if (user == null)
            {
                throw new NotFoundException();
            }

            user.Username = item.Username;

            if (user.IsValid)
            {
                _userDataMapper.Update(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, user.ValidationErrors);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        /// <remarks>
        /// DELETE users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        /// </remarks>
        public HttpResponseMessage Delete(Guid id)
        {
            _userDataMapper.Delete(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}