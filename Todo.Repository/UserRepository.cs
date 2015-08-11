using AutoMapper;
using Todo.DAL;
using Todo.DAL.Models;
using Todo.Repository.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Repository
{
    public class UserRepository : IUserRepository, IUserManagerFactory
    {
        private readonly IRepository repository;
        private readonly IUserManagerFactory userManagerFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserRepository(IRepository repository, IUserManagerFactory userManagerFactory)
        {
            this.repository = repository;
            this.userManagerFactory = userManagerFactory;
        }

        #region Other methods

        public UserManager<UserEntity> CreateUserManager()
        {
            return userManagerFactory.CreateUserManager();
        }

        /// <summary>
        /// Initalize user manager
        /// </summary>
        /// <returns>new user manager</returns>
        private UserManager<UserEntity> createUserManager()
        {
            return new UserManager<UserEntity>(new UserStore<UserEntity>(new TodoContext()));
        }

        #endregion

        #region Get


        public Task<ICollection<Todo.Model.Common.IUser>> GetAsync()
        {
            return Task.FromResult(Mapper.Map<ICollection<Todo.Model.Common.IUser>>(repository.Where<UserEntity>()));
        }

        /// <summary>
        ///  Get by id
        /// </summary>
        public async Task<Todo.Model.Common.IUser> GetAsync(string username)
        {
            try
            {
                return Mapper.Map<Todo.Model.Common.IUser>(await repository.GetAsync<UserEntity>(c => c.UserName == username));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Get all entites
        /// </summary>
        public async Task<IEnumerable<Todo.Model.Common.IUser>> GetAsync(System.Linq.Expressions.Expression<Func<Todo.Model.Common.IUser, bool>> match)
        {
            try
            {
                return Mapper.Map<IEnumerable<Todo.Model.Common.IUser>>(await repository.GetRangeAsync<UserEntity>());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Find user by username and password
        /// </summary>
        public async Task<Todo.Model.Common.IUser> GetAsync(string username, string password)
        {
            try
            {
                UserEntity user;
                UserManager<UserEntity> userManager = CreateUserManager();

                user = await userManager.FindAsync(username, password);

                return Mapper.Map<Todo.Model.Common.IUser>(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Add

        /// <summary>
        /// Add user
        /// </summary>
        public async Task<int> AddAsync(Todo.Model.Common.IUser user)
        {
            try
            {
                
                return await repository.AddAsync<UserEntity>(Mapper.Map<UserEntity>(user));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Registers adds user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if success, false otherwise</returns>
        public async Task<bool> RegisterUser(Todo.Model.Common.IUser user, string password)
        {
            try
            {
                
                UserManager<UserEntity> userManager = this.CreateUserManager();

                IdentityResult result = await userManager.CreateAsync(Mapper.Map<UserEntity>(user), password);

                return result.Succeeded;
            

            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        #endregion

        #region Delete

        /// <summary>
        /// Delete user
        /// </summary>
        public async Task<int> DeleteAsync(Todo.Model.Common.IUser user)
        {
            try
            {
                return await repository.DeleteAsync<UserEntity>(Mapper.Map<UserEntity>(user));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Delete by id
        /// </summary>
        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await this.DeleteAsync(Mapper.Map<Todo.Model.Common.IUser>
                    (await repository.GetAsync<UserEntity>(id)));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region  Update

        /// <summary>
        /// Updates user, return int as result
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="password">User password for validation</param>
        /// <returns>Int { 0: operation failed }</returns>
        public async Task<int> UpdateAsync(Todo.Model.Common.IUser user)
        {
            try
            {
                return await repository.UpdateAsync<UserEntity>(Mapper.Map<UserEntity>(user));
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Updates user and return updated user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <param name="password">User password</param>
        /// <returns>IUser</returns>
        public async Task<Todo.Model.Common.IUser> UpdateUserAsync(Todo.Model.Common.IUser user, string password)
        {
            try
            {
                IUnitOfWork uow = repository.CreateUnitOfWork();
                bool passwordValid = false;
                Task<UserEntity> result = null;

                // Check if user is valid
                UserManager<UserEntity> UserManager = CreateUserManager();

                UserEntity userToCheck = await UserManager.FindByIdAsync(user.Id);
                passwordValid = await UserManager.CheckPasswordAsync(userToCheck, password);


                if (passwordValid)
                    result = uow.UpdateWithAddAsync<UserEntity>(Mapper.Map<UserEntity>(user));
                else
                    throw new Exception("Invalid password");

                await uow.CommitAsync();
                return await Task.FromResult(Mapper.Map<Todo.Model.Common.IUser>(result.Result) as Todo.Model.Common.IUser);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates only username or email
        /// </summary>
        /// <param name="user">UserToUpdate</param>
        /// <param name="password">User password</param>
        /// <returns>IUser</returns>
        public async Task<Todo.Model.Common.IUser> UpdateUserEmailOrUsernameAsync(Todo.Model.Common.IUser user, string password)
        {
            try
            {
                IUnitOfWork uow = repository.CreateUnitOfWork();
                bool passwordValid = false;
                Task<UserEntity> result = null;

                // Check if user is valid
                UserManager<UserEntity> UserManager = CreateUserManager();
                
                    UserEntity userToCheck = await UserManager.FindByIdAsync(user.Id);
                    passwordValid = await UserManager.CheckPasswordAsync(userToCheck, password);
               

                if (passwordValid)
                    result = uow.UpdateWithAddAsync<UserEntity>(Mapper.Map<UserEntity>(user), u => u.Email, u => u.UserName);
                else
                    throw new Exception("Invalid password.");

                await uow.CommitAsync();
                return await Task.FromResult(Mapper.Map<Todo.Model.Common.IUser>(result.Result) as Todo.Model.Common.IUser);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="user">User </param>
        /// <param name="newPassword">New password</param>
        /// <returns>IUser</returns>
        public async Task<bool> UpdateUserPasswordAsync(string userId, string oldPassword, string newPassword)
        {
            try
            {

                IdentityResult result;
                UserManager<UserEntity> UserManager = CreateUserManager();

                result = await UserManager.ChangePasswordAsync(userId, oldPassword, newPassword);


                return result.Succeeded;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }

    public interface IUserManagerFactory
    {
        UserManager<UserEntity> CreateUserManager();
    }
}
