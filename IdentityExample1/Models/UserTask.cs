using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample1.Models
{
    public class UserTask
    {
        private int id;
        private int ownerId;
        private string description;
        private DateTime dueDate;
        private int completed;

        public int Id { get => id; set => id = value; }
        public int OwnerId { get => ownerId; set => ownerId = value; }
        public string Description { get => description; set => description = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
        public int Completed { get => completed; set => completed = value; }
    }
}
