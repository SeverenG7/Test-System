using System;
using TestSystem.Model.Models;
using TestSystem.DataProvider.IdentityManager;
using System.Threading.Tasks;
using TestSystem.DataProvider.Context;

namespace TestSystem.DataProvider.Interfaces
{
   public interface IUnitOfWorkUpdate
    {
        IRepository<Test> Tests { get; }
        IRepository<Question> Questions { get; }
        IRepository<Theme> Themes { get; }
        int Complete();

        Task SaveAsync();
    }
}
