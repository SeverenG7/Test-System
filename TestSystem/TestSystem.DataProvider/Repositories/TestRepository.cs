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
    public class TestRepository : Repository<Test>
    {
        public TestRepository(ApplicationContext context) : base(context)
        { }

        public new void Add(Test test)
        {
            foreach (Question question in test.Questions)
            {
                testContext.Entry(question).State = System.Data.Entity.EntityState.Unchanged;
            }

            testContext.Entry(test.Theme).State = System.Data.Entity.EntityState.Unchanged;
        }

        public ApplicationContext testContext
        {
            get => context as ApplicationContext;
        }
    }
}
