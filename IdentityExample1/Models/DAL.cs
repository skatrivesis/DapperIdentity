using Dapper;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<Tasks> GetTasksByMostRecent(int id)
        {
            var queryString = "SELECT * FROM Tasks WHERE Userid = @id";

            return conn.Query<Tasks>(queryString, new { id = id });
        }

        [HttpPost]
        public int FlipStatus(Tasks t)
        {
            if (t.Completed == 1)
            {
                t.Completed = 0;
            }
            else
            {
                t.Completed = 1;
            }
            var flipQuery = "UPDATE Tasks SET Completed = @Completed WHERE Id = @id";

            return conn.Execute(flipQuery, t);
        }
    }
}
