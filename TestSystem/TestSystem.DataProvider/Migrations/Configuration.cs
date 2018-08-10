namespace TestSystem.DataProvider.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TestSystem.DataProvider.Context.ApplicationContext";
        }

        protected override void Seed(Context.ApplicationContext context)
        {
        }
    }
}
