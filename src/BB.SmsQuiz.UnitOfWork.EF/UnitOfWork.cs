// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BB.SmsQuiz.Infrastructure.UnitOfWork;
using BB.SmsQuiz.UnitOfWork.EF.Models;

namespace BB.SmsQuiz.Repository.EF
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly BBSmsQuizDatabaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            _context = new BBSmsQuizDatabaseContext();
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        public IContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}