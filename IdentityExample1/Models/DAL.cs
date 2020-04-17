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

        public Tasks GetTasksById(int id)
        {
            var queryString = "SELECT * FROM Tasks WHERE Id = @id";
            return conn.QueryFirstOrDefault<Tasks>(queryString, new { id = id });
        }

        public int FlipStatus(Tasks t)
        {
            if (t.Completed == 1)
            {
                t.Completed = 0;
            }
            else if (t.Completed == 0)
            {
                t.Completed = 1;
            }
            else
            {
                t.Completed = 0;
            }

            var flipQuery = "UPDATE Tasks SET Completed = @Completed WHERE Id = @id";

            return conn.Execute(flipQuery, t);
        }

        public int DeleteTaskById(int id)
        {
            string deleteQuery = "DELETE FROM Tasks WHERE Id = @id";

            return conn.Execute(deleteQuery, new { id = id });
        }

        public int CreateTask(Tasks t)
        {
            t.Completed = 0;

            string addQuery = "INSERT INTO Tasks (UserId, TaskDescription, DueDate, Completed)";
            addQuery += "VALUES (@UserId, @TaskDescription, @DueDate, @Completed)";
            return conn.Execute(addQuery, t);
        }
    }
}
