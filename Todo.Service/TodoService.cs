using Todo.Common;
using Todo.Model.Common;
using Todo.Repository.Common;
using Todo.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Service
{
    /// <summary>
    /// Todo service
    /// </summary>
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">ITodoRepository object</param>
        public TodoService(ITodoRepository repository)
        {
            this.repository = repository;
        }

        #region Methods
        /// <summary>
        /// Get todo/task
        /// </summary>
        /// <param name="id">Id to find by</param>
        /// <returns>Todo</returns>
        public async Task<ITodo> GetAsync(Guid id)
        {
            return await repository.GetAsync(id);
        }

        /// <summary>
        /// Get collection of todos/tasks
        /// </summary>
        /// <param name="filer">Filter options</param>
        /// <returns>Todo collection</returns>
        public async Task<ICollection<Model.Common.ITodo>> GetRangeAsync(GenericPaging filter)
        {
            return await repository.GetRangeAsync(filter);
        }

        /// <summary>
        /// Get collection of todos
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="search">String to search by</param>
        /// <returns>Todo colletion</returns>
        public async Task<ICollection<ITodo>> GetRangeAsync(GenericPaging filter, string search)
        {
            return await repository.GetRangeAsync(filter, search);
        }

        /// <summary>
        /// Get collection of todos
        /// </summary>
        /// <param name="filter">Filter options</param>
        /// <param name="userId">Get task/todo that belongs to logged user</param>
        /// <returns>Todo colletion</returns>
        public async Task<ICollection<ITodo>> GetTaskAsync(GenericPaging filter, string userId)
        {
            return await repository.GetTaskAsync(filter, userId);
        }

        /// <summary>
        /// Add todo async
        /// </summary>
        /// <param name="todo">Todo to add</param>
        /// <returns>The number of objects written to the underlying database.</returns>
        public async Task<int> AddAsync(Model.Common.ITodo todo)
        {
            return await repository.AddAsync(todo);
        }

        public Task<int> UpdateAsync(Model.Common.ITodo todo)
        {
            return repository.UpdateAsync(todo);
        }

        /// <summary>
        /// Delete posted task/todo
        /// </summary>
        public async Task<int> DeleteTodo(Model.Common.ITodo todo)
        {
            try
            {
                return await repository.DeletAsync(todo);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> DeleteTodo(Guid id)
        {
            try
            {
                return await repository.DeleteAsync(id);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
