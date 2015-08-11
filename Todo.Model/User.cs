using Todo.Model.Common;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Todo.Model
{
    /// <summary>
    /// Business logic model, inherits from identity user
    /// </summary>
    public class User : IdentityUser, IUser
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

    }
}
