using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Todo.DAL.Models;

namespace Todo.DAL
{
    public interface ITodoContext : IDisposable
    {  
        /// <summary>
        /// Provides method signatures for database context
        /// </summary>
        DbSet<TodoEntity> Todos { get; set; }
       
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
