using Todo.Common;
using Todo.Model.Common;
using System;


namespace Todo.Mvc.Models
{
    /// <summary>
    /// Todo model
    /// </summary>
    public class TodoModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string AssignedToUserId { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }

        public IUser User { get; set; }
    }
}