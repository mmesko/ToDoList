using System.Collections.Generic;

namespace TodoList.Model.Common
{
    public interface IUser
    {
        string Id { get; set; }
        string UserName { get; set; }
        string PasswordHash { get; set; }
        string Email { get; set; }

        // One to many
        ICollection<ITodoList> TodoLists { get; set; }
    }
}
