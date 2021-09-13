using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOLibrary
{
    public class Connection
    {
        private readonly string _connectionString;

        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        /*
         ExecuteScalar() => return une valeur
         ExecuteNonQuery() => return le nombre de row affectée
         ExecuteReader() => return un jeu d'enregistrement / minimum 1 voir un tableau d'enregistrement

         SqlConnection

         SqlCommand
         */

        public IEnumerable<TEntity> ExecuteReader<TEntity>(Command cmd, Func<TEntity, SqlDataReader> convert)
        {

        }

        // connection.ExectureReader<Contact>(cmd, Conversion);

        // public Contact Conversion(SqlDataReader reader) {
        //  return new Contact
        //{
        //    Id = (int) reader["Id"],
        //    FirstName = reader["FirstName"].ToString(),
        //    LastName = reader["LastName"].ToString(),
        //    Email = reader["Email"].ToString()
        //};
    // }

    private SqlConnection createConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }

        private SqlCommand createCommand(Command cmd, SqlConnection connection)
        {
            SqlCommand sqlCmd = connection.CreateCommand();
            sqlCmd.CommandText = cmd._query;
            sqlCmd.CommandType = cmd._isStoredProcedure ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;
            if(!(cmd._parameters is null))
            {
                foreach (KeyValuePair<string, object> param in cmd._parameters)
                {
                    SqlParameter parameter = sqlCmd.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value;

                    sqlCmd.Parameters.Add(parameter);
                }
            }
            return sqlCmd;
        }
    }
}
