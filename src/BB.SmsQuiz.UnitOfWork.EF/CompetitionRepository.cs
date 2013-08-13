// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionRepository.cs" company="contentedcoder.com">
//   contentedcoder.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BB.SmsQuiz.Infrastructure.UnitOfWork;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.States;
using BB.SmsQuiz.UnitOfWork.EF.Models;
using Competition = BB.SmsQuiz.Model.Competitions.Competition;

namespace BB.SmsQuiz.Repository.EF
{
    /// <summary>
    /// The competition repository.
    /// </summary>
    public class CompetitionRepository : ICompetitionRepository
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly BBSmsQuizDatabaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public CompetitionRepository(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork.Context as BBSmsQuizDatabaseContext;
        }

        /// <summary>
        /// The get by competition key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition GetByCompetitionKey(string key)
        {
            return CompetitionMapper.MapFrom(_context.Competitions.SingleOrDefault(c => c.CompetitionKey == key));
        }

        /// <summary>
        /// The get by status.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Competition> GetByStatus(CompetitionStatus status)
        {
            return CompetitionMapper.MapFrom(_context.Competitions.Where(c => c.Status == (byte) status));
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(Competition item)
        {
            item.ID = Guid.NewGuid();
            _context.Entry(CompetitionMapper.MapTo(item)).State = EntityState.Added;
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Remove(Competition item)
        {
            var data = CompetitionMapper.MapTo(item);
            _context.Competitions.Remove(data);
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Update(Competition item)
        {
            var data = CompetitionMapper.MapTo(item);
            var updating = _context.Competitions.Find(item.ID);

            var newEntrant = data.Entrants.SingleOrDefault(e => e.ID == Guid.Empty);

            if (newEntrant != null)
            {
                newEntrant.ID = Guid.NewGuid();
                updating.Entrants.Add(newEntrant);
            }

            _context.Entry(updating).CurrentValues.SetValues(data);
        }

        /// <summary>
        /// The find by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition FindById(Guid id)
        {
            return CompetitionMapper.MapFrom(_context.Competitions.SingleOrDefault(u => u.ID == id));
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Competition> GetAll()
        {
            return CompetitionMapper.MapFrom(_context.Competitions);
        }

        /// <summary>
        /// The delete method.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void Delete(Guid id)
        {
            var item = _context.Competitions.Find(id);
            var t = item.PossibleAnswers;
            _context.Competitions.Remove(item);
        }
    }
}