using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Todo.Model.Common;

namespace Todo.Repository.Common
{
    /// <summary>
    /// Defines method signatures for user repository
    /// </summary>
    public interface IUserRepository
    {
        // Get
        Task<IUser> GetAsync(string username);
        Task<IEnumerable<IUser>> GetAsync(Expression<Func<IUser, bool>> match);
        Task<IUser> GetAsync(string username, string password);
        Task<ICollection<Model.Common.IUser>> GetAsync();

        // Add
        Task<int> AddAsync(IUser user);
        Task<bool> RegisterUser(IUser user, string password);

        // Update
        Task<int> UpdateAsync(IUser user);
        Task<IUser> UpdateUserAsync(IUser user, string password);
        Task<IUser> UpdateUserEmailOrUsernameAsync(IUser user, string password);
        Task<bool> UpdateUserPasswordAsync(string userId, string oldPassword, string newPassword);

        // Delete
        Task<int> DeleteAsync(IUser user);
        Task<int> DeleteAsync(Guid id);

    }
}
