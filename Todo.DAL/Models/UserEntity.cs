using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.DAL.Models
{
    /// <summary>
    /// Database entity
    /// </summary>
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

        [Index(IsUnique = true)]
        public override string UserName { get; set; }

        //One to many relationship
        public virtual ICollection<TodoEntity> Todos { get; set; }
    }
}
