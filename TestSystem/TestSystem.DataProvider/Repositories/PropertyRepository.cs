﻿using TestSystem.Model.Models;
using TestSystem.DataProvider.ContextData;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public class PropertyRepository : Repository<Property>
    {
        public PropertyRepository(TestContext context) : base(context)
        { }

        public TestContext testContext
        {
            get => context as TestContext;
        }
    }
}
}
