using Todo.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Service.Common
{
    /// <summary>
    /// Defines method signatures for user service
    /// </summary>
    public interface IUserService
    {
        Task<IUser> FindAsync(string username);
        Task<IUser> FindAsync(string username, string password);
        Task<ICollection<IUser>> GetAsync();
        Task<bool> RegisterUser(IUser user, string password);
        Task<IUser> UpdateEmailOrUsernameAsync(IUser user, string password);
        Task<bool> UpdatePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}
