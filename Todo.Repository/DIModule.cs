using Todo.DAL;
using Todo.DAL.Models;
using Todo.Repository.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Extensions.Factory;

namespace Todo.Repository
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        /// <summary>
        /// Loads module
        /// </summary>
        public override void Load()
        {
            #region Bindings

            // Context, generic repo, unit of work
            Bind<ITodoContext>().To<TodoContext>();
            Bind<IRepository>().To<Repository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUnitOfWorkFactory>().ToFactory();

            // Bind user store and manager
            Bind<UserManager<UserEntity>>().ToSelf().WithConstructorArgument(typeof(IUserStore<UserEntity>), new UserStore<UserEntity>(new TodoContext()));
            Bind<IUserManagerFactory>().ToFactory();
;

            // Other repositories
            Bind<ITodoRepository>().To<TodoRepository>();
            Bind<IUserRepository>().To<UserRepository>();


            #endregion

        }
    }
}
