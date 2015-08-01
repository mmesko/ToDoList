using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.DAL.Models
{
    public class UserEntity : IdentityUser
    {
        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                // Set to new guid only if string is empty or null
                if (String.IsNullOrEmpty(value))
                    base.Id = Guid.NewGuid().ToString();
                else
                    base.Id = value;
            }
        }

        //needed for identity
        [Index(IsUnique = true)]
        public override string UserName { get; set; }

        //one to many 
        public virtual ICollection<TodoListEntity> TodoLists { get; set; }
    }
}
