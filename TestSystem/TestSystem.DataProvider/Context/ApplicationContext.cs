using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestSystem.Model.Models;

namespace TestSystem.DataProvider.Context
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext() : base("name=TestDB")
        { }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<TempResult> TempResults { get; set; }
        public virtual DbSet<UserQuestion> UserQuestions { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }
    }
}
