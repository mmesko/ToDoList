using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Todo.Mvc.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class UserModel : IdentityUser
    {
        public UserModel()
        {
            Id = Guid.NewGuid().ToString();    
        }

        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    base.Id = Guid.NewGuid().ToString();
                else
                    base.Id = value;
            }
        }
    }
}