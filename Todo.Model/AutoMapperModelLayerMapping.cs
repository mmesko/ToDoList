using AutoMapper;
using Todo.DAL.Models;
using Todo.Model.Common;

namespace Todo.Model
{
    /// <summary>
    /// Provides mappings for model layer, entites from data base layer and model interfaces
    /// </summary>
    public class AutomapperModelLayerMapping : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<ITodo, TodoEntity>().ReverseMap();
            Mapper.CreateMap<TodoEntity, Todo>().ReverseMap();

            Mapper.CreateMap<IUser, UserEntity>().ReverseMap();
            Mapper.CreateMap<UserEntity, User>().ReverseMap();
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
