using System.Linq;
using System.Reflection;
using System.Text;

namespace BB.SmsQuiz.Repository.Dapper
{
    internal sealed class DynamicQuery
    {
        /// <summary>
        /// Gets the insert query.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The Sql query based on the item properties of the anonymous type.
        /// </returns>
        internal static string GetInsertQuery(string tableName, dynamic item)
        {
            PropertyInfo[] props = item.GetType().GetProperties();
            string[] columns = props.Select(p => p.Name).Where(s => s != "ID").ToArray();

            return string.Format("INSERT INTO {0} ({1}) OUTPUT inserted.ID VALUES (@{2})",
                                    tableName,
                                    string.Join(",", columns),
                                    string.Join(",@", columns));
        }

        /// <summary>
        /// Gets the update query.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="item">The item.</param>
        /// <returns>
        /// The Sql query based on the item properties of the anonymous type.
        /// </returns>
        internal static string GetUpdateQuery(string tableName, dynamic item)
        {
            PropertyInfo[] props = item.GetType().GetProperties();
            string[] columns = props.Select(p => p.Name).ToArray();

            var parameters = columns.Select(name => name + "=@" + name).ToList();

            return string.Format("UPDATE {0} SET {1} WHERE ID=@ID", tableName, string.Join(",", parameters));
        }

        /// <summary>
        /// Gets the where query.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The where clause of a query from an anonymous type.</returns>
        internal static string GetWhereQuery(dynamic item)
        {
            PropertyInfo[] props = item.GetType().GetProperties();
            string[] columns = props.Select(p => p.Name).ToArray();

            var builder = new StringBuilder();

            for (int i = 0; i < columns.Count(); i++)
            {
                string col = columns[i];
                builder.Append(col);
                builder.Append("=@");
                builder.Append(col);

                if (i < columns.Count() - 1)
                {
                    builder.Append(" AND ");
                }
            }

            return builder.ToString();
        }
    }
}
