using Todo.Service.Common;


namespace Todo.Service
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        /// <summary>
        /// Load modules into kernel
        /// </summary>
        public override void Load()
        {
          
            Bind<IUserService>().To<UserService>();
            Bind<ITodoService>().To<TodoService>();
            
        }
    }
}
