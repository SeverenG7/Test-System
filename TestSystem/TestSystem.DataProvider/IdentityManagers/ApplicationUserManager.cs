using Microsoft.AspNet.Identity;
using TestSystem.Model.Models;

namespace TestSystem.DataProvider.IdentityManagers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {

        }
    }
}
