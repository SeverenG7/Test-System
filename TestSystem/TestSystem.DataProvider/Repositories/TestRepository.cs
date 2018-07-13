using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Model.Models;
using TestSystem.DataProvider.ContextData;
using System.Data.Entity;

namespace TestSystem.DataProvider.Repositories
{
    public class TestRepository : IRepository<Test>
    {
        private readonly TestContext context;

        public TestRepository(TestContext _context)
        {
            context = _context;
        }

        public IEnumerable<Test> GetAll() => context.Tests;

        public Test Get(int id) => context.Tests.Find(id);

        public IEnumerable<Test> Find(Func<Test, bool> predicate)
        {
            return context.Tests.Where(predicate).ToList();
        }

        public void Create(Test test) => context.Tests.Add(test);

        public void Update(Test test)
        {
            context.Entry(test).State = EntityState.Modified;

        }

        public void Delete(int id)
        {
            Test test = context.Tests.Find(id);
            if (test != null)
                context.Tests.Remove(test);

        }

    }
}
