using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace TestSystem.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Results = new HashSet<Result>();
        }

        public virtual UserInfo UserInfo { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
