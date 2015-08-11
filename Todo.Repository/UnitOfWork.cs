using Todo.DAL;
using Todo.Repository.Common;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Todo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ITodoContext DbContext { get; private set; }

        public UnitOfWork(ITodoContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context cannot be null");
            }

            DbContext = context;
        }

        #region Methods

        /// <summary>
        /// Adds entity, commit should be called afterwards to save changes
        /// </summary>
        /// <returns>T entity</returns>
        public virtual Task<T> AddAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry dbEntity = DbContext.Entry(entity);
                if (dbEntity.State != EntityState.Detached)
                {
                    dbEntity.State = EntityState.Added;
                }
                else
                {
                    DbContext.Set<T>().Add(entity);
                }
                return Task.FromResult(dbEntity.Entity as T);
            }
            catch (Exception)
            {
                throw;
            }
        }

       

        

        /// <summary>
        /// Update without adding new entites
        /// </summary>
        public virtual Task<T> UpdateWithAttachAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry entry = DbContext.Entry<T>(entity);
                entry.State = EntityState.Modified;

                return Task.FromResult(entry.Entity as T);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// Updates entity, commit should be called afterwards to save changes
        /// </summary>
        /// <returns>T entity</returns>
        public virtual Task<T> UpdateWithAddAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry entityEntry = DbContext.Entry<T>(entity);
                DbContext.Set<T>().Add(entity);
                entityEntry.State = EntityState.Modified;

                return Task.FromResult<T>(entityEntry.Entity as T);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity to update</param>
        /// <param name="entityParameters">Entity parameters to update</param>
        public virtual Task<T> UpdateWithAddAsync<T>(T entity, params Expression<Func<T, object>>[] entityParameters) where T : class
        {
            try
            {
                DbEntityEntry entry = DbContext.Entry<T>(entity);
                DbContext.Set<T>().Add(entity);
                entry.State = EntityState.Unchanged;
                foreach (var p in entityParameters)
                {
                    DbContext.Entry<T>(entity).Property(p).IsModified = true;
                }

                return Task.FromResult(entry.Entity as T);
            }
            catch (Exception)
            {

                throw;
            }
        }


       

       

        /// <summary>
        /// Deletes entity,commit should be called afterwards to save changes
        /// </summary>
        public virtual Task<int> DeleteAsync<T>(T entity) where T : class
        {
            try
            {
                DbEntityEntry entityEntry = DbContext.Entry(entity);
                if (entityEntry.State != EntityState.Deleted)
                {
                    entityEntry.State = EntityState.Deleted;
                }
                return Task.FromResult(1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete entity, commit should be called afterwards to save changes
        /// </summary>
        public Task<int> DeleteAsync<T>(Guid id) where T : class
        {
            try
            {
                T entity = DbContext.Set<T>().Find(id);
                if (entity == null)
                {
                    return Task.FromResult(0);
                }
                return DeleteAsync<T>(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Delete entity, commit should be called afterwards to save changes
        /// </summary>
        public Task<int> DeleteAsync<T>(Expression<Func<T, bool>> match) where T : class
        {
            try
            {
                T entity = DbContext.Set<T>().Where(match).First();
                if (entity == null)
                {
                    return Task.FromResult(0);
                }
                return DeleteAsync<T>(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

    

       

        /// <summary>
        /// Save changes to database
        /// </summary>
        public async Task<int> CommitAsync()
        {
            return await DbContext.SaveChangesAsync();
        }


        #endregion

        #region Dispose implementation

        public void Dispose()
        {
            DbContext.Dispose();
        }

        #endregion





    }
}
