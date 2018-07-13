namespace TestSystem.DataProvider.ContextData
{
    using System.Data.Entity;
    using Model.Models;

    public  class TestContext : DbContext
    {
        public TestContext()
            : base("name=TestContext")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<UserSystem> UserSystemes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .Property(e => e.AnswerText)
                .IsUnicode(false);

            modelBuilder.Entity<Answer>()
                .HasMany(e => e.Question)
                .WithMany(e => e.Answer)
                .Map(m => m.ToTable("QuestionAnswer").MapLeftKey("IdAnswer").MapRightKey("IdQuestion"));

            modelBuilder.Entity<Question>()
                .Property(e => e.QuestionText)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Test)
                .WithMany(e => e.Question)
                .Map(m => m.ToTable("TestQuestion").MapLeftKey("IdQuestion").MapRightKey("IdTest"));

            modelBuilder.Entity<Result>()
                .Property(e => e.ResultDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Test>()
                .Property(e => e.TestDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.Result)
                .WithRequired(e => e.Test)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theme>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Theme>()
                .HasMany(e => e.Property)
                .WithRequired(e => e.Theme)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.UserSystem)
                .WithRequired(e => e.UserInfo)
                .WillCascadeOnDelete(false);
        }
    }
}
