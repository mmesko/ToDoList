using System;

namespace Todo.Model.Common
{   
    /// <summary>
    /// Provides minimum implementation for todo/task entity
    /// </summary>
    public interface ITodo
    {
        Guid Id { get; set; }
        string UserId { get; set; }
        string AssignedToUserId { get; set; }
        string Title { get; set; }
        string TaskDescription { get; set; }
        bool IsCompleted { get; set; }
        DateTime DateCreated { get; set; }
        
        IUser User { get; set; }
    }
}
