using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using BB.SmsQuiz.Infrastructure.Domain;
using Dapper;

namespace BB.SmsQuiz.Repository.Dapper
{
    /// <summary>
    /// Base repository that wraps the Dapper Micro ORM
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// For more information regarding Dapper see: http://code.google.com/p/dapper-dot-net/
    /// </remarks>
    public abstract class Repository<T> : IRepository<T> where T : EntityBase, IAggregateRoot
    {
        /// <summary>
        /// The _table name
        /// </summary>
        private readonly string _tableName;

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["SmsQuizConnection"].ConnectionString);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        protected Repository(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// Mapping the object to the insert/update columns.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The parameters with values.</returns>
        /// <remarks>In the default case, we take the object as is with no custom mapping.</remarks>
        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Add(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                item.ID = cn.Insert<Guid>(_tableName, parameters);
            }
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Update(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                cn.Update(_tableName, parameters);
            }
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Remove(T item)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM " + _tableName + " WHERE ID=@ID", new { ID = item.ID });
            }
        }

        /// <summary>
        /// Finds by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual T FindByID(Guid id)
        {
            T item;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE ID=@ID", new { ID = id }).SingleOrDefault();
            }

            return item;
        }

        /// <summary>
        /// Finds the specified predicate.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="param">The param.</param>
        /// <returns>
        /// A list of items
        /// </returns>
        public virtual IEnumerable<T> Find(string query, dynamic param)
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE " + query, (object)param);
            } 

            return items;
        }

        /// <summary>
        /// Finds the specified param.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Find(dynamic param)
        {
            return Find(DynamicQuery.GetWhereQuery(param), param);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>All items</returns>
        public virtual IEnumerable<T> FindAll()
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName);
            }

            return items;
        }
    }
}
