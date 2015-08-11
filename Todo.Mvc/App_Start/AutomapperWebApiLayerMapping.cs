
using AutoMapper;
using Todo.Model;
using Todo.Model.Common;
using Todo.Mvc.Models;


namespace Todo.Mvc.App_Start
{
    public class AutoMapperWebApiLayerMapping : Profile
    {
        protected override void Configure()
        {
            // Todo
            Mapper.CreateMap<ITodo, TodoModel>().ReverseMap();
            Mapper.CreateMap<Model.Todo, TodoModel>().ReverseMap();

            // User 
            Mapper.CreateMap<IUser, UserModel>().ReverseMap();
            Mapper.CreateMap<User, UserModel>().ReverseMap();
        }

        public override string ProfileName
        {
            get
            {
                return this.GetType().Name;
            }
        }
    }
}