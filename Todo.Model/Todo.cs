using Todo.Model.Common;
using System;

namespace Todo.Model
{
    /// <summary>
    /// Todo/Task model
    /// </summary>
    public class Todo : ITodo
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string AssignedToUserId { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateCreated { get; set; }
     
        public IUser User { get; set; }
    }
}
