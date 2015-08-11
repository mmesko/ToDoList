using System;

namespace Todo.Model.Common
{
    /// <summary>
    /// Interface for user
    /// </summary>
    public interface IUser
    {
        string Id { get; set; }
        string UserName { get; set; }
        string PasswordHash { get; set; }
        string Email { get; set; }

    }
}
