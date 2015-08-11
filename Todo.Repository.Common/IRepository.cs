using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Todo.Repository.Common
{

    /// <summary>
    /// Interface with repository implementation methods
    /// </summary>
    public interface IRepository
    {

        IUnitOfWork CreateUnitOfWork();

        Task<T> GetAsync<T>(Guid id) where T : class;

        IQueryable<T> Where<T>() where T : class;

        Task<IEnumerable<T>> GetRangeAsync<T>() where T : class;

        Task<T> GetAsync<T>(Expression<Func<T, bool>> match) where T : class;

        Task<IEnumerable<T>> GetRangeAsync<T>(Expression<Func<T, bool>> match) where T : class;

        Task<int> AddAsync<T>(T entity) where T : class;

        Task<int> AddAsync<T>(IEnumerable<T> listOfEntities) where T : class;

        Task<int> DeleteAsync<T>(T entity) where T : class;

        Task<int> DeleteAsync<T>(Guid id) where T : class;

        Task<int> DeleteRangeAsync<T>(IEnumerable<T> entites) where T : class;

        Task<int> UpdateAsync<T>(T entity) where T: class;

        Task<int> UpdateWithAddAsync<T>(T entity) where T : class;

        Task<int> UpdateAsync<T>(T entity, params Expression<Func<T, object>>[] proportiesToUpdate) where T : class;

 


    }
}
