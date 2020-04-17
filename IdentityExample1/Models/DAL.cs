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

        public IEnumerable<Tasks> GetTasksBySortASC(int id)
        {
            var queryString = "SELECT * FROM Tasks WHERE Userid = @id ORDER BY Completed ASC";

            return conn.Query<Tasks>(queryString, new { id = id });
        }
        public IEnumerable<Tasks> GetTasksBySortDESC(int id)
        {
            var queryString = "SELECT * FROM Tasks WHERE Userid = @id ORDER BY Completed DESC";

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
            var deleteQuery = "DELETE FROM Tasks WHERE Id = @id";

            return conn.Execute(deleteQuery, new { id = id });
        }

        public int CreateTask(Tasks t)
        {
            t.Completed = 0;

            var addQuery = "INSERT INTO Tasks (UserId, TaskDescription, DueDate, Completed)";
            addQuery += "VALUES (@UserId, @TaskDescription, @DueDate, @Completed)";
            return conn.Execute(addQuery, t);
        }

        public int EditTask(Tasks t)
        {
            var editString = "UPDATE Tasks SET TaskDescription = @TaskDescription WHERE Id = @Id";

            return conn.Execute(editString, t);
        }

        public IEnumerable<Tasks> Search(string search)
        {
            search = '%' + search.ToLower() + '%';

            var searchString = "SELECT * FROM Tasks WHERE TaskDescription LIKE @search";

            return conn.Query<Tasks>(searchString, new { search = search });
        }
    }
}
