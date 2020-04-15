using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample1.Models
{
    public class DAL
    {
        private SqlConnection conn;

        public DAL(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }

        public IEnumerable<Task> GetTasksByMostRecent(string userId)
        {
            string queryString = "SELECT * FROM Tasks WHERE Userid = @userId";

            return conn.Query<Task>(queryString, new { userId = userId });
        }
    }
}
