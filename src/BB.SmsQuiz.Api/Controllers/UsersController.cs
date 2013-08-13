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
        /// The _unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The _encryption service.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        /// The _user repository.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        /// <param name="mapper">
        /// The mapper.
        /// </param>
        /// <param name="encryptionService">
        /// The encryption service.
        /// </param>
        public UsersController(
            IUnitOfWork unitOfWork, 
            IUserRepository userRepository, 
            IMapper mapper,        
            IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
            _encryptionService = encryptionService;
        }

        // GET users
        public IEnumerable<GetUser> Get()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<IEnumerable<User>, IEnumerable<GetUser>>(users);
        }

        // GET users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public GetUser Get(Guid id)
        {
            var user = _userRepository.FindById(id);
            
            if (user == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<User, GetUser>(user);
        }

        // POST users
        public HttpResponseMessage Post(PostUser item)
        {
            var user = new User()
                {
                    Username = item.Username, 
                    Password = EncryptedString.Create(item.Password, _encryptionService)
                };

            if (user.IsValid)
            {
                _userRepository.Add(user);
                _unitOfWork.SaveChanges();

                GetUser createdItem = _mapper.Map<User, GetUser>(user);
                return CreatedHttpResponse(createdItem.ID, createdItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, user.ValidationErrors);
        }

        // PUT users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Put(Guid id, PutUser item)
        {
            User user = _userRepository.FindById(id);

            if (user == null)
            {
                throw new NotFoundException();
            }

            user.Username = item.Username;

            if (user.IsValid)
            {
                _userRepository.Update(user);
                _unitOfWork.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, user.ValidationErrors);
        }

        // DELETE users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Delete(Guid id)
        {
            _userRepository.Delete(id);
            _unitOfWork.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}