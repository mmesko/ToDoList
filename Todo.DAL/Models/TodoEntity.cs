using System;

namespace Todo.DAL.Models
{
   public class TodoEntity
    {

        public Guid Id { get; set; }
        public string UserId { get; set; } //User that created task
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
        public string AssignedToUserId { get; set; } //task assigned to user

        public DateTime DateCreated { get; set; }
        public virtual UserEntity User { get; set; }

    }
}
