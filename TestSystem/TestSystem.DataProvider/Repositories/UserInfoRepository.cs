﻿using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    /// <summary>
    /// Realization of generic Repository class.
    /// Also, here can add specific IRepository interfaces for entites,
    /// if this will be needed.
    /// </summary>
    public class UserInfoRepository : Repository<UserInfo>
    {
        public UserInfoRepository(ApplicationContext context) : base(context)
        { }

        public ApplicationContext testContext
        {
            get => context as ApplicationContext;
        }
    }
}

