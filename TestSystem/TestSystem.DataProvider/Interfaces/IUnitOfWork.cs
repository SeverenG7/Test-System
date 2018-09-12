using System;
using TestSystem.Model.Models;
using TestSystem.DataProvider.IdentityManagers;
using System.Threading.Tasks;

namespace TestSystem.DataProvider.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Test> Tests { get; }
        IRepository<Question> Questions { get; }
        IRepository<Answer> Answers { get; }
        IRepository<Result> Results { get; }
        IRepository<UserInfo> UserInfoes { get; }
        IRepository<Theme> Themes { get; }
        IRepository<TempResult> TempResults { get; }
        IRepository<UserQuestion> UserQuestions { get; }
        IRepository<UserAnswer> UserAnswers { get; }

        ApplicationUserManager ApplicationUserManagers { get; }
        ApplicationRoleManager ApplicationRoleManagers { get; }

        int Complete();

        Task SaveAsync();
    }
}
