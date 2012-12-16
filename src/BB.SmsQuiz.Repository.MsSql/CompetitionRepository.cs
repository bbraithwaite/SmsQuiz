using System;
using BB.SmsQuiz.Model.Competitions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace BB.SmsQuiz.Repository.MsSql
{
    public class CompetitionRepository : ICompetitionRepository
    {
        public void Add(Competition item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Competition item)
        {
            throw new NotImplementedException();
        }

        public void Update(Competition item)
        {
            throw new NotImplementedException();
        }

        public Competition FindByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Competition> Find(Expression<Func<Competition, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Competition> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
