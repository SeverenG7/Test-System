﻿using TestSystem.Model.Models;
using TestSystem.DataProvider.ContextData;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    /// <summary>
    /// Realization of generic Repository class.
    /// Also, here can add specific IRepository interfaces for entites,
    /// if this will be needed.
    /// </summary>
    public class TestRepository : Repository<Test>
    {
        public TestRepository(TestContext context) : base(context)
        { }

        public TestContext testContext
        {
            get => context as TestContext;
        }
    }
}
