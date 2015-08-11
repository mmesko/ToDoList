using Todo.Common;
using Todo.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Service.Common
{
    /// <summary>
    /// Provides method signatures for todoc service
    /// </summary>
    public interface ITodoService
    {
        Task<ITodo> GetAsync(Guid id);
        Task<ICollection<ITodo>> GetRangeAsync(GenericPaging filter);
        Task<ICollection<ITodo>> GetRangeAsync(GenericPaging filter, string search);
        Task<ICollection<ITodo>> GetTaskAsync(GenericPaging filter, string userId);
        Task<int> AddAsync(ITodo todo);
        Task<int> UpdateAsync(ITodo todo);
        Task<int> DeleteTodo(ITodo todo);
        Task<int> DeleteTodo(Guid id);
    }
}
