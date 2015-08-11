using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Common;
using Todo.Model.Common;

namespace Todo.Repository.Common
{
    /// <summary>
    /// Defines method signatures for todo repository
    /// </summary>
    public interface ITodoRepository
    {
        Task<ITodo> GetAsync(Guid id);
        Task<ICollection<ITodo>> GetRangeAsync(GenericPaging filter);

        Task<ICollection<ITodo>> GetRangeAsync(GenericPaging filter, string search);
        Task<ICollection<ITodo>> GetTaskAsync(GenericPaging filter, string userId);
        Task<int> AddAsync(ITodo todo);
        Task<int> UpdateAsync(ITodo todo);
        Task<int> DeletAsync(ITodo todo);
        Task<int> DeleteAsync(Guid id);

    }
}
