using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BB.SmsQuiz.Api.Filters;
using BB.SmsQuiz.ApiModel.Users;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Users;

namespace BB.SmsQuiz.Api.Controllers
{
    [UnhandledException, TokenValidationAttribute]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encryptionService = encryptionService;
        }

        // GET users
        public IEnumerable<GetUser> Get()
        {
            var users = _userRepository.FindAll();
            return _mapper.Map<IEnumerable<User>, IEnumerable<GetUser>>(users);
        }

        // GET users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public GetUser Get(Guid id)
        {
            var user = _userRepository.FindByID(id);
            if (user == null) throw new NotFoundException();
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

                GetUser createdItem = _mapper.Map<User, GetUser>(user);
                return CreatedHttpResponse(createdItem.ID, createdItem);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, user.ValidationErrors);
        }

        // PUT users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Put(Guid id, PutUser item)
        {
            User user = _userRepository.FindByID(id);

            if (user == null) throw new NotFoundException();

            user.Username = item.Username;

            if (user.IsValid)
            {
                _userRepository.Update(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, user.ValidationErrors);
        }

        // DELETE users/B5608F8E-F449-E211-BB40-1040F3A7A3B1
        public HttpResponseMessage Delete(Guid id)
        {
            var user = _userRepository.FindByID(id);

            if (user == null) throw new NotFoundException();

            _userRepository.Remove(user);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}