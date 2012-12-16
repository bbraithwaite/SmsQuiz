using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Repository.NoSql
{
    public abstract class Repository<T> : IRepository<T> where T : EntityBase, IAggregateRoot
    {
        private Dictionary<Guid, T> _db = new Dictionary<Guid,T>();

        public void Add(T item)
        {
            item.ID = Guid.NewGuid();
            _db.Add(item.ID, item);
        }

        public void Remove(T item)
        {
            _db.Remove(item.ID);
        }

        public void Update(T item)
        {
            _db[item.ID] = item;
        }

        public T FindByID(System.Guid id)
        {
            if (_db.ContainsKey(id))
                return _db[id];

            return default(T);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _db.Values.Where(predicate.Compile()).AsQueryable();
        }

        public IEnumerable<T> FindAll()
        {
            return _db.Values.AsQueryable();
        }
    }
}
