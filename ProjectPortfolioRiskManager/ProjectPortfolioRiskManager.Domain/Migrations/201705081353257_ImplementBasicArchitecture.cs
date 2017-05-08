namespace ProjectPortfolioRiskManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ImplementBasicArchitecture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    QuestionnaireId = c.Int(nullable: false),
                    QuestionId = c.Int(nullable: false),
                    LikertItemId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LikertItems", t => t.LikertItemId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Questionnaires", t => t.QuestionnaireId)
                .Index(t => t.QuestionnaireId)
                .Index(t => t.QuestionId)
                .Index(t => t.LikertItemId);

            CreateTable(
                "dbo.LikertItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Value = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Templates_LikertItems",
                c => new
                {
                    TemplateId = c.Int(nullable: false),
                    LikertItemId = c.Int(nullable: false),
                    OrderNumber = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.TemplateId, t.LikertItemId })
                .ForeignKey("dbo.LikertItems", t => t.LikertItemId, cascadeDelete: true)
                .ForeignKey("dbo.Templates", t => t.TemplateId, cascadeDelete: true)
                .Index(t => t.TemplateId)
                .Index(t => t.LikertItemId);

            CreateTable(
                "dbo.Templates",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Content = c.String(),
                    CreationUserId = c.String(nullable: false, maxLength: 128),
                    LastUpdateUserId = c.String(nullable: false, maxLength: 128),
                    CreationDate = c.DateTime(nullable: false),
                    LastUpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreationUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.LastUpdateUserId)
                .Index(t => t.CreationUserId)
                .Index(t => t.LastUpdateUserId);

            CreateTable(
                "dbo.CompanySizes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Value = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Questionnaires",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TemplateId = c.Int(nullable: false),
                    CompanySizeId = c.Int(nullable: false),
                    PositionId = c.Int(nullable: false),
                    Industry = c.String(),
                    UserId = c.String(nullable: false, maxLength: 128),
                    CreationDate = c.DateTime(nullable: false),
                    LastUpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompanySizes", t => t.CompanySizeId)
                .ForeignKey("dbo.Positions", t => t.PositionId)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TemplateId)
                .Index(t => t.CompanySizeId)
                .Index(t => t.PositionId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Positions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Value = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Sections",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    TemplateId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Templates", t => t.TemplateId)
                .Index(t => t.TemplateId);

            CreateTable(
                "dbo.Questions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Value = c.String(),
                    SectionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId);

            CreateTable(
                "dbo.Templates_CompanySizes",
                c => new
                {
                    TemplateRefId = c.Int(nullable: false),
                    CompanySizeRefId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.TemplateRefId, t.CompanySizeRefId })
                .ForeignKey("dbo.Templates", t => t.TemplateRefId, cascadeDelete: true)
                .ForeignKey("dbo.CompanySizes", t => t.CompanySizeRefId, cascadeDelete: true)
                .Index(t => t.TemplateRefId)
                .Index(t => t.CompanySizeRefId);

            CreateTable(
                "dbo.Templates_Positions",
                c => new
                {
                    TemplateRefId = c.Int(nullable: false),
                    PositionRefId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.TemplateRefId, t.PositionRefId })
                .ForeignKey("dbo.Templates", t => t.TemplateRefId, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.PositionRefId, cascadeDelete: true)
                .Index(t => t.TemplateRefId)
                .Index(t => t.PositionRefId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionnaireId", "dbo.Questionnaires");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "LikertItemId", "dbo.LikertItems");
            DropForeignKey("dbo.Templates_LikertItems", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.Sections", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.Questions", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Templates_Positions", "PositionRefId", "dbo.Positions");
            DropForeignKey("dbo.Templates_Positions", "TemplateRefId", "dbo.Templates");
            DropForeignKey("dbo.Templates", "LastUpdateUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Templates", "CreationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Templates_CompanySizes", "CompanySizeRefId", "dbo.CompanySizes");
            DropForeignKey("dbo.Templates_CompanySizes", "TemplateRefId", "dbo.Templates");
            DropForeignKey("dbo.Questionnaires", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questionnaires", "TemplateId", "dbo.Templates");
            DropForeignKey("dbo.Questionnaires", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Questionnaires", "CompanySizeId", "dbo.CompanySizes");
            DropForeignKey("dbo.Templates_LikertItems", "LikertItemId", "dbo.LikertItems");
            DropIndex("dbo.Templates_Positions", new[] { "PositionRefId" });
            DropIndex("dbo.Templates_Positions", new[] { "TemplateRefId" });
            DropIndex("dbo.Templates_CompanySizes", new[] { "CompanySizeRefId" });
            DropIndex("dbo.Templates_CompanySizes", new[] { "TemplateRefId" });
            DropIndex("dbo.Questions", new[] { "SectionId" });
            DropIndex("dbo.Sections", new[] { "TemplateId" });
            DropIndex("dbo.Questionnaires", new[] { "UserId" });
            DropIndex("dbo.Questionnaires", new[] { "PositionId" });
            DropIndex("dbo.Questionnaires", new[] { "CompanySizeId" });
            DropIndex("dbo.Questionnaires", new[] { "TemplateId" });
            DropIndex("dbo.Templates", new[] { "LastUpdateUserId" });
            DropIndex("dbo.Templates", new[] { "CreationUserId" });
            DropIndex("dbo.Templates_LikertItems", new[] { "LikertItemId" });
            DropIndex("dbo.Templates_LikertItems", new[] { "TemplateId" });
            DropIndex("dbo.Answers", new[] { "LikertItemId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Answers", new[] { "QuestionnaireId" });
            DropTable("dbo.Templates_Positions");
            DropTable("dbo.Templates_CompanySizes");
            DropTable("dbo.Questions");
            DropTable("dbo.Sections");
            DropTable("dbo.Positions");
            DropTable("dbo.Questionnaires");
            DropTable("dbo.CompanySizes");
            DropTable("dbo.Templates");
            DropTable("dbo.Templates_LikertItems");
            DropTable("dbo.LikertItems");
            DropTable("dbo.Answers");
        }
    }
}
