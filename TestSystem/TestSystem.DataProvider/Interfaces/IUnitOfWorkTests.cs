using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Model.Models;

namespace TestSystem.DataProvider.Interfaces
{
    public interface IUnitOfWorkTests : IDisposable
    {
        IRepository<Test> Tests { get; }
        IRepository<Question> Questions { get; }
        IRepository<Answer> Answers { get; }

        void Save();
    }
}
