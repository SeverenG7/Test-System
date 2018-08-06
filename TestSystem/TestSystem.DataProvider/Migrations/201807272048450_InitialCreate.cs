namespace TestSystem.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        IdAnswer = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(nullable: false, unicode: false, storeType: "text"),
                        Correct = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdAnswer);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        IdQuestion = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(nullable: false, unicode: false, storeType: "text"),
                        QuestionImage = c.Binary(storeType: "image"),
                        AnswerNumber = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        IdProperty = c.Int(),
                    })
                .PrimaryKey(t => t.IdQuestion)
                .ForeignKey("dbo.Property", t => t.IdProperty)
                .Index(t => t.IdProperty);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        IdProperty = c.Int(nullable: false, identity: true),
                        Difficult = c.Int(nullable: false),
                        IdTheme = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdProperty)
                .ForeignKey("dbo.Theme", t => t.IdTheme, cascadeDelete: true)
                .Index(t => t.IdTheme);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        IdTest = c.Int(nullable: false, identity: true),
                        TestName = c.String(nullable: false, maxLength: 50),
                        QuestionsNumber = c.Int(nullable: false),
                        TestDescription = c.String(unicode: false, storeType: "text"),
                        IdProperty = c.Int(),
                    })
                .PrimaryKey(t => t.IdTest)
                .ForeignKey("dbo.Property", t => t.IdProperty)
                .Index(t => t.IdProperty);
            
            CreateTable(
                "dbo.Result",
                c => new
                    {
                        IdResult = c.Int(nullable: false, identity: true),
                        UserLogin = c.String(nullable: false, maxLength: 20),
                        IdTest = c.Int(nullable: false),
                        ResultScore = c.Double(nullable: false),
                        ResultDescription = c.String(unicode: false, storeType: "text"),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdResult)
                .ForeignKey("dbo.Test", t => t.IdTest, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdTest)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Theme",
                c => new
                    {
                        IdTheme = c.Int(nullable: false, identity: true),
                        ThemeName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.IdTheme);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        IdUserInfo = c.String(nullable: false, maxLength: 128),
                        UserFirstName = c.String(nullable: false, maxLength: 20),
                        UserLastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUserInfo)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUserInfo)
                .Index(t => t.IdUserInfo);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        Question_IdQuestion = c.Int(nullable: false),
                        Answer_IdAnswer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_IdQuestion, t.Answer_IdAnswer })
                .ForeignKey("dbo.Question", t => t.Question_IdQuestion, cascadeDelete: true)
                .ForeignKey("dbo.Answer", t => t.Answer_IdAnswer, cascadeDelete: true)
                .Index(t => t.Question_IdQuestion)
                .Index(t => t.Answer_IdAnswer);
            
            CreateTable(
                "dbo.TestQuestions",
                c => new
                    {
                        Test_IdTest = c.Int(nullable: false),
                        Question_IdQuestion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Test_IdTest, t.Question_IdQuestion })
                .ForeignKey("dbo.Test", t => t.Test_IdTest, cascadeDelete: true)
                .ForeignKey("dbo.Question", t => t.Question_IdQuestion, cascadeDelete: true)
                .Index(t => t.Test_IdTest)
                .Index(t => t.Question_IdQuestion);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInfo", "IdUserInfo", "dbo.AspNetUsers");
            DropForeignKey("dbo.Result", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Property", "IdTheme", "dbo.Theme");
            DropForeignKey("dbo.Result", "IdTest", "dbo.Test");
            DropForeignKey("dbo.TestQuestions", "Question_IdQuestion", "dbo.Question");
            DropForeignKey("dbo.TestQuestions", "Test_IdTest", "dbo.Test");
            DropForeignKey("dbo.Test", "IdProperty", "dbo.Property");
            DropForeignKey("dbo.Question", "IdProperty", "dbo.Property");
            DropForeignKey("dbo.QuestionAnswers", "Answer_IdAnswer", "dbo.Answer");
            DropForeignKey("dbo.QuestionAnswers", "Question_IdQuestion", "dbo.Question");
            DropIndex("dbo.TestQuestions", new[] { "Question_IdQuestion" });
            DropIndex("dbo.TestQuestions", new[] { "Test_IdTest" });
            DropIndex("dbo.QuestionAnswers", new[] { "Answer_IdAnswer" });
            DropIndex("dbo.QuestionAnswers", new[] { "Question_IdQuestion" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserInfo", new[] { "IdUserInfo" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Result", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Result", new[] { "IdTest" });
            DropIndex("dbo.Test", new[] { "IdProperty" });
            DropIndex("dbo.Property", new[] { "IdTheme" });
            DropIndex("dbo.Question", new[] { "IdProperty" });
            DropTable("dbo.TestQuestions");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserInfo");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Theme");
            DropTable("dbo.Result");
            DropTable("dbo.Test");
            DropTable("dbo.Property");
            DropTable("dbo.Question");
            DropTable("dbo.Answer");
        }
    }
}
