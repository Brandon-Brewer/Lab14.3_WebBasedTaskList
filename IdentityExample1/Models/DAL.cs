using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace IdentityExample1.Models
{
    public class DAL
    {
        private SqlConnection conn;

        public DAL(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }

        public int CreateTask(UserTask t)
        {
            string queryString = "INSERT INTO IdentityExample1.dbo.Tasks (OwnerId, Description, DueDate, Completed) ";
            queryString += "VALUES (@OwnerId, @Description, @DueDate, @Completed)";

            return conn.Execute(queryString, t);
        }

        public IEnumerable<UserTask> GetTasks(string id)
        {
            string queryString = "SELECT * FROM Tasks WHERE OwnerId = @id ORDER BY DueDate";
            return conn.Query<UserTask>(queryString, new { id = id });
        }

        public IEnumerable<UserTask> GetTasksCompleted(string id)
        {
            string queryString = "SELECT * FROM Tasks WHERE Completed = 1 AND OwnerId = @id ORDER BY DueDate";
            return conn.Query<UserTask>(queryString, new { id = id });
        }

        public IEnumerable<UserTask> GetTasksNotCompleted(string id)
        {
            string queryString = "SELECT * FROM Tasks WHERE Completed = 0 AND OwnerId = @id ORDER BY DueDate";
            return conn.Query<UserTask>(queryString, new { id = id });
        }

        public UserTask GetTaskById(int id)
        {
            string queryString = "SELECT * FROM Tasks WHERE Id = @id ORDER BY DueDate";
            return conn.QueryFirstOrDefault<UserTask>(queryString, new { id = id });
        }

        public int MarkAsComplete(UserTask t)
        {
            string queryString = "UPDATE Tasks SET Completed = 1 WHERE Id = @Id";
            return conn.Execute(queryString, t);
        }

        public int DeleteTask(UserTask t)
        {
            string queryString = "Delete FROM Tasks WHERE Id = @Id";
            return conn.Execute(queryString, t);
        }

        public IEnumerable<UserTask> SearchTask(string search, string id)
        {
            string queryString;

            if (DateTime.TryParse(search, out DateTime s))
            {
                queryString = "SELECT * FROM Tasks WHERE DueDate = @search AND OwnerId = @id ORDER BY DueDate";
            }
            else
            {
                search = '%' + search + '%';
                queryString = "SELECT * FROM Tasks WHERE Description LIKE @search AND OwnerId = @id ORDER BY DueDate";
            }

            return conn.Query<UserTask>(queryString, new { search = search, id = id });
        }

        public IEnumerable<UserTask> SortTasksByDueDate(string id)
        {
            string queryString = "SELECT * FROM Tasks WHERE OwnerId = @id ORDER BY DueDate";
            return conn.Query<UserTask>(queryString, new { id = id });
        }

        public IEnumerable<UserTask> SortTasksByDueDateDESC(string id)
        {
            string queryString = "SELECT * FROM Tasks WHERE OwnerId = @id ORDER BY DueDate DESC";
            return conn.Query<UserTask>(queryString, new { id = id });
        }

        public int EditTaskById(UserTask t)
        {
            string queryString = "UPDATE Tasks SET OwnerId = @OwnerId, Description = @Description, DueDate = @DueDate, Completed = @Completed WHERE Id = @Id";
            return conn.Execute(queryString, t);
        }
    }
}