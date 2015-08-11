using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Todo.DAL.Mapping;
using Todo.DAL.Models;

namespace Todo.DAL
{
    /// <summary>
    /// Todo/Task context for communication with database
    /// </summary>
    public class TodoContext : IdentityDbContext<UserEntity>, ITodoContext
    {
        #region Constructors

        /// <summary>
        /// The Constructor
        /// </summary>
        public TodoContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            base.RequireUniqueEmail = true;
        }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="connection">Database name or connection string</param>
        public TodoContext(string connection)
            : base(connection, throwIfV1Schema: false)
        {
            base.RequireUniqueEmail = true;
        }

        #endregion

        #region Proporties

      
        public DbSet<TodoEntity> Todos { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Database configuration options and mappings
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new TodoMap());

            //Not necessary at the moment bc there is no many-to many relationship yet
            modelBuilder.Conventions.Add<ManyToManyCascadeDeleteConvention>();

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

        //writing exceptions if sth goes wrong
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        #endregion
    }
}
