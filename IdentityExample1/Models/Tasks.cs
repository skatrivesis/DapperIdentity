using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample1.Models
{
    public class Tasks
    {
        private int id;
        private int userId;
        private string taskDescription;
        private DateTime dueDate;
        private int completed;

        public int Id { get => id; set => id = value; }
        public int UserId { get => userId; set => userId = value; }
        public string TaskDescription { get => taskDescription; set => taskDescription = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public int Completed { get => completed; set => completed = value; }
    }
}
