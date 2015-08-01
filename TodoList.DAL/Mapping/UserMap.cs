using TodoList.DAL.Models;
using System.Data.Entity.ModelConfiguration;

namespace TodoList.DAL.Mapping
{
    public class UserMap : EntityTypeConfiguration<UserEntity>
    {
        public UserMap()
        {
            //relationship constraints no needed, CreatedUserId and AssignedUserId are FK in Tasklist
            //see TasklistMap for details
          

        }
    }
}
