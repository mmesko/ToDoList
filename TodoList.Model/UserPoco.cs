using TaskList.Model.Common;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace TaskList.Model
{
    public class UserPoco : IdentityUser, IUser
    {
        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                if (String.IsNullOrEmpty(Id))
                    base.Id = Guid.NewGuid().ToString();
                else
                    base.Id = value;
            }
        }

        // One to many
        public virtual ICollection<ITasklist> Tasklists { get; set; }

    }
}
