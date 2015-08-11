using AutoMapper;
using Todo.Common;
using Todo.DAL.Models;
using Todo.Model.Common;
using Todo.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Repository
{
    /// <summary>
    /// Todo/task repository
    /// </summary>
    public class TodoRepository : ITodoRepository
    {
        private readonly IRepository repository;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="repository">IRepository member</param>
        public TodoRepository(IRepository repository)
        {
            this.repository = repository;
        }

        #region Methods

        /// <summary>
        /// Get task/todo  by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>ITodo</returns>
        public async Task<Model.Common.ITodo> GetAsync(Guid id)
        {
            try
            {
                return Mapper.Map<ITodo>(await repository.Where<TodoEntity>()
                    .Where(item => item.Id == id)
                    .Include(item => item.User)
                    .SingleOrDefaultAsync());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Get collection of todos/tasks
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <returns>Collection of todos</returns>
        public async Task<ICollection<Model.Common.ITodo>> GetRangeAsync(GenericPaging filter)
        {
            try
            {
                if (filter == null)
                    filter = new GenericPaging(1, 5);

                return Mapper.Map<ICollection<ITodo>>(await repository.Where<TodoEntity>()
                    .Include(item => item.User)
                    .OrderBy(t => t.Title)
                    .Skip((filter.PageNumber * filter.PageSize) - filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Get collection of todos/tasks
        /// </summary>
        /// <param name="filter">Filter with page number and size</param>
        /// <param name="search">Search string</param>
        /// <returns>Todos collection</returns>
        public async Task<ICollection<ITodo>> GetRangeAsync(GenericPaging filter, string search)
        {
            try
            {
                if (filter == null)
                    filter = new GenericPaging(1, 5);
               
                return Mapper.Map<ICollection<ITodo>>(await repository.Where<TodoEntity>()
                    .Where(t => t.Title.Contains(search))
                    .OrderBy(t => t.Title)
                    .Skip((filter.PageNumber * filter.PageSize) - filter.PageSize)
                    .Take(filter.PageSize)
                    .Include(item => item.User)
                    .ToListAsync());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Get collection of todos
        /// </summary>
        /// <param name="filter">Filter with pagenumber and size</param>
        /// <param name="search">Search userId</param>
        /// <returns>Todos collection</returns>
        public async Task<ICollection<ITodo>> GetTaskAsync(GenericPaging filter, string userId)
        {
            try
            {
                if (filter == null)
                    filter = new GenericPaging(1, 5);

                return Mapper.Map<ICollection<ITodo>>(await repository.Where<TodoEntity>()
                    .Where(t => t.UserId==userId || t.AssignedToUserId == userId)
                    .OrderBy(t => t.Title)
                    .Skip((filter.PageNumber * filter.PageSize) - filter.PageSize)
                    .Take(filter.PageSize)
                    .Include(item => item.User)
                    .ToListAsync());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


     

        /// <summary>
        /// Add new todo
        /// </summary>
        /// <param name="todo">ITodo implemented entity</param>
        /// <returns>The number of objects written to the underlying database.</returns>
        public Task<int> AddAsync(Model.Common.ITodo todo)
        {
            try
            {
                return repository.AddAsync<TodoEntity>(Mapper.Map<TodoEntity>(todo));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// Update async
        /// </summary>
        /// <param name="todoc">Todo/Task to update</param>
        /// <returns>The number of objects written to the underlying database.</returns>
        public Task<int> UpdateAsync(Model.Common.ITodo todo)
        {
            try
            {
                return repository.UpdateAsync<TodoEntity>(Mapper.Map<TodoEntity>(todo));
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        /// <summary>
        /// Delete async
        /// </summary>
        public async Task<int> DeletAsync(Model.Common.ITodo todo)
        {
            try
            {
                return await repository.DeleteAsync<TodoEntity>(Mapper.Map<TodoEntity>(todo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete async
        /// </summary>
        /// <param name="id">Id to delete by</param>
        /// <returns>The number of objects written to the underlying database.</returns>
        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await repository.DeleteAsync<TodoEntity>(id);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        #endregion
    }
}
