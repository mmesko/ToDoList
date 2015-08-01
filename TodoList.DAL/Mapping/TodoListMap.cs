using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TodoList.DAL.Models;


namespace TodoList.DAL.Mapping
{
    public class TodoListMap : EntityTypeConfiguration<TodoListEntity>
    {
        public TodoListMap()
        {

            //primary key
             HasKey(g => g.Id);

            //properties
            Property(g => g.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(g => g.Title).IsRequired().HasMaxLength(50);
            Property(g => g.Description).IsRequired().HasMaxLength(255).HasColumnType("nvarchar");
            Property(g => g.DateCreated).IsRequired().HasColumnType("datetime2");
            Property(g => g.DateCompleted).IsRequired().HasColumnType("datetime2");
            Property(g => g.IsActive).IsRequired();
            
            //one to many relationships
            HasRequired(g => g.User).WithMany(u => u.TodoLists).HasForeignKey(g => g.CreatedByUserId);
            HasRequired(g => g.User).WithMany(u => u.TodoLists).HasForeignKey(g => g.AssignedByUserId);

           
        }
    }
}
