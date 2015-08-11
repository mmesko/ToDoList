using Todo.DAL.Models;
using System.Data.Entity.ModelConfiguration;

namespace Todo.DAL.Mapping
{
    public class UserMap : EntityTypeConfiguration<UserEntity>
    {
        public UserMap()
        {
            //relationship constraints no needed
            //inherits from Identity
 

        }
    }
}