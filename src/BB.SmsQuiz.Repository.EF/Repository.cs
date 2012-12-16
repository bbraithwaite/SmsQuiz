using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BB.SmsQuiz.Infrastructure.Domain;

namespace BB.SmsQuiz.Repository.EF
{
    public class Repository<T> : IRepository<T> where T : EntityBase, IAggregateRoot
    {
        private Context _context;
        private DbSet<T> _set = null;

        public DbSet<T> Set
        {
            get
            {
                if (_set == null)
                {
                    _set = this._context.Set<T>();
                }
                return _set;
            }
        }

        public Repository()
        {
            _context = new Context();
        }

        public void Add(T item)
        {
            this.Set.Add(item);
            _context.SaveChanges();
        }

        public void Remove(T item)
        {
            this.Set.Remove(item);
            _context.SaveChanges();
        }

        public void Update(T item)
        {
            _context.SaveChanges();
        }

        public T FindByID(Guid id)
        {
            return this.Set.SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.Set.Where(predicate);
        }

        public IEnumerable<T> FindAll()
        {
            return this.Set.AsQueryable();
        }
    }
}
