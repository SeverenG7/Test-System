namespace TestSystem.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Question", "IdProperty", "dbo.Property");
            DropForeignKey("dbo.Test", "IdProperty", "dbo.Property");
            DropForeignKey("dbo.Property", "IdTheme", "dbo.Theme");
            DropIndex("dbo.Question", new[] { "IdProperty" });
            DropIndex("dbo.Property", new[] { "IdTheme" });
            DropIndex("dbo.Test", new[] { "IdProperty" });
            AddColumn("dbo.Question", "IdTheme", c => c.Int());
            AddColumn("dbo.Question", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Question", "Difficult", c => c.String(nullable: false));
            AddColumn("dbo.Test", "IdTheme", c => c.Int());
            AddColumn("dbo.Test", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Test", "Difficult", c => c.String(nullable: false));
            AddColumn("dbo.Result", "CreateDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Question", "IdTheme");
            CreateIndex("dbo.Test", "IdTheme");
            AddForeignKey("dbo.Question", "IdTheme", "dbo.Theme", "IdTheme");
            AddForeignKey("dbo.Test", "IdTheme", "dbo.Theme", "IdTheme");
            DropColumn("dbo.Question", "IdProperty");
            DropColumn("dbo.Test", "IdProperty");
            DropTable("dbo.Property");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        IdProperty = c.Int(nullable: false, identity: true),
                        Difficult = c.Int(nullable: false),
                        IdTheme = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProperty);
            
            AddColumn("dbo.Test", "IdProperty", c => c.Int());
            AddColumn("dbo.Question", "IdProperty", c => c.Int());
            DropForeignKey("dbo.Test", "IdTheme", "dbo.Theme");
            DropForeignKey("dbo.Question", "IdTheme", "dbo.Theme");
            DropIndex("dbo.Test", new[] { "IdTheme" });
            DropIndex("dbo.Question", new[] { "IdTheme" });
            DropColumn("dbo.Result", "CreateDate");
            DropColumn("dbo.Test", "Difficult");
            DropColumn("dbo.Test", "CreateDate");
            DropColumn("dbo.Test", "IdTheme");
            DropColumn("dbo.Question", "Difficult");
            DropColumn("dbo.Question", "CreateDate");
            DropColumn("dbo.Question", "IdTheme");
            CreateIndex("dbo.Test", "IdProperty");
            CreateIndex("dbo.Property", "IdTheme");
            CreateIndex("dbo.Question", "IdProperty");
            AddForeignKey("dbo.Property", "IdTheme", "dbo.Theme", "IdTheme", cascadeDelete: true);
            AddForeignKey("dbo.Test", "IdProperty", "dbo.Property", "IdProperty");
            AddForeignKey("dbo.Question", "IdProperty", "dbo.Property", "IdProperty");
        }
    }
}
