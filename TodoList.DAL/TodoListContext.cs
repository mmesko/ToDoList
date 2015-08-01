using TodoList.DAL.Mapping;
using TodoList.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TodoList.DAL
{
    /// <summary>
    /// Task List context for communication with database
    /// </summary>
    public class TodoListContext : IdentityDbContext<UserEntity>, ITodoListContext
    {
        #region Constructors
        static TodoListContext()
        {
            Database.SetInitializer<TodoListContext>(null);
        }

        public TodoListContext()
            : base("Name=DefaultConnection", throwIfV1Schema: false)
        {
        }

        #endregion

        #region Proporties
        public DbSet<TodoListEntity> TodoLists { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Database configuration options and mappings
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TodoListMap());
            modelBuilder.Configurations.Add(new UserMap());
           

            base.OnModelCreating(modelBuilder);
        }


        // Summary:
        //     A DbSet represents the collection of all entities in the context, or that
        //     can be queried from the database, of a given type. DbSet objects are created
        //     from a DbContext using the DbContext.Set method.
        //
        // Type parameters:
        //   TEntity:
        //     The type that defines the set.
        //
        // Remarks:
        //     Note that DbSet does not support MEST (Multiple Entity Sets per Type) meaning
        //     that there is always a one-to-one correlation between a type and a set
        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }


        #endregion
    }
}
