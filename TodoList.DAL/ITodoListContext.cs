using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using TodoList.DAL.Models;

namespace TodoList.DAL
{
    /// <summary>
    /// Provides method signatures for database context
    /// </summary>
    public interface ITodoListContext : IDisposable
    {
        //DbSet<UserEntity> Users { get; set; } is not needed because we use identity
        DbSet<TodoListEntity> TodoLists { get; set; }


        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync();
    }
}
