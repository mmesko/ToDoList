using Todo.DAL.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.DAL.Mapping
{
    public class TodoMap : EntityTypeConfiguration<TodoEntity>
    {
        public TodoMap()
        {  
            //Primary key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Title).IsRequired().HasMaxLength(100);
            Property(t => t.TaskDescription).IsRequired().HasMaxLength(255);
            Property(t => t.IsCompleted);
            Property(p => p.DateCreated).HasColumnType("date");
            Property(p => p.AssignedToUserId).IsRequired().HasMaxLength(128);
          
            // Relationship
            HasRequired(t => t.User).WithMany(u => u.Todos).HasForeignKey(t => t.UserId);
          

        }
    }
}