// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BB.SmsQuiz.Infrastructure.UnitOfWork;
using BB.SmsQuiz.Model.Users;
using BB.SmsQuiz.UnitOfWork.EF.Models;
using User = BB.SmsQuiz.Model.Users.User;

namespace BB.SmsQuiz.Repository.EF
{
    /// <summary>
    /// The user repository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly BBSmsQuizDatabaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The context.
        /// </param>
        public UserRepository(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context as BBSmsQuizDatabaseContext;
        }

        /// <summary>
        /// The find by username.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="Model.Users.User"/>.
        /// </returns>
        public User FindByUsername(string username)
        {
            return UserMapper.MapFrom(_context.Users.SingleOrDefault(u => u.Username == username));
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(User item)
        {
            item.ID = Guid.NewGuid();
            _context.Entry(UserMapper.MapTo(item)).State = EntityState.Added;
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Remove(User item)
        {
            var data = UserMapper.MapTo(item);
            _context.Users.Remove(data);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Update(User item)
        {
            var data = UserMapper.MapTo(item);
            var updating = _context.Users.Find(data.ID);
            _context.Entry(updating).CurrentValues.SetValues(data);
        }

        /// <summary>
        /// The find by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User FindById(Guid id)
        {
            return UserMapper.MapFrom(_context.Users.SingleOrDefault(u => u.ID == id));
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<User> GetAll()
        {
            return UserMapper.MapFrom(_context.Users);
        }

        /// <summary>
        /// The delete method.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void Delete(Guid id)
        {
            var item = _context.Users.Find(id);
            _context.Users.Remove(item);
        }
    }
}