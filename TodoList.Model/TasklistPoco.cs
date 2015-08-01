using System;
using TaskList.Model.Common;

namespace TaskList.Model
{
    public class TasklistPoco : ITasklist
    {
        //PK
        public string Id { get; set; }

        //Foreign keys
        //Id that belongs to user who created task
        public string CreatedByUserId { get; set; }
        //Id that belongs to user assigned to task
        public string AssignedByUserId { get; set; }

        //Other properties
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
        public bool IsActive { get; set; }


        // Many to one, TaskList can have one user, user can have many tasks
        public virtual IUser User { get; set; }
    }
}
