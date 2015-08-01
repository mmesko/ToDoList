using System;

namespace TodoList.Model.Common
{
    public interface ITodoList
    {
        string Id { get; set; }
        string CreatedByUserId { get; set; }     
        string AssignedByUserId { get; set; }     
        string Title { get; set; }
        string Description { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateCompleted { get; set; }
        bool IsActive { get; set; }

        IUser User { get; set; }

    }
}
