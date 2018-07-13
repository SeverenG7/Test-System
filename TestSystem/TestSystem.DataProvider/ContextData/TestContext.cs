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

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<Theme> Theme { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserSystem> UserSystem { get; set; }

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
