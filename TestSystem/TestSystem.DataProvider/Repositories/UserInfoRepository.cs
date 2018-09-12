using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public class UserInfoRepository : Repository<UserInfo>
    {
        public UserInfoRepository(ApplicationContext context) : base(context)
        { }
    }
}

