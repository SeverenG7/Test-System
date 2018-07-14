using System;
using TestSystem.Model.Models;

namespace TestSystem.DataProvider.Interfaces
{
    /// <summary>
    /// IUnitOfWork repository for just one concrete class UnitOfWork
    /// which garantee that every repository will use one context
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// IRepositories with all entity in Model project
        /// </summary>
        IRepository<Test> Tests { get; }
        IRepository<Question> Questions { get; }
        IRepository<Answer> Answers { get; }
        IRepository<Property> Properties {get;}
        IRepository<Result> Results { get; }
        IRepository<UserSystem> Users { get; }
        IRepository<UserInfo> UserInfoes { get; }
        IRepository<Theme> Themes { get; }
        /// <summary>
        /// Method Complete for save changes in database
        /// </summary>
        /// <returns></returns>
        int Complete();
    }
}
