namespace ProjectPortfolioRiskManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertInitialQuestionnaireData : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Templates", new[] { "CreationUserId" });
            DropIndex("dbo.Templates", new[] { "LastUpdateUserId" });
            RenameColumn(table: "dbo.Templates_LikertItems", name: "LikertItemId", newName: "LikertItemRefId");
            RenameColumn(table: "dbo.Templates_LikertItems", name: "TemplateId", newName: "TemplateRefId");
            RenameIndex(table: "dbo.Templates_LikertItems", name: "IX_TemplateId", newName: "IX_TemplateRefId");
            RenameIndex(table: "dbo.Templates_LikertItems", name: "IX_LikertItemId", newName: "IX_LikertItemRefId");
            AlterColumn("dbo.Templates", "CreationUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Templates", "LastUpdateUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Templates", "CreationDate", c => c.DateTime());
            AlterColumn("dbo.Templates", "LastUpdateDate", c => c.DateTime());
            CreateIndex("dbo.Templates", "CreationUserId");
            CreateIndex("dbo.Templates", "LastUpdateUserId");

            Sql(@"
                INSERT INTO Templates(Content) values('');
            ");

            Sql(@"
                INSERT INTO CompanySizes(Value) values('micro (fewer than 10 employees and an annual turnover (the amount of money taken in a particular period) or balance sheet (a statement of a company''s assets and liabilities) below €2 million.)');
                INSERT INTO CompanySizes(Value) values('small (fewer than 50 employees and an annual turnover or balance sheet below €10 million)');
                INSERT INTO CompanySizes(Value) values('medium-sized enterprise (fewer than 250 employees and annual turnover below €50 million or balance sheet below €43 million)');
                INSERT INTO CompanySizes(Value) values('large enterprise');
            ");

            Sql(@"
                INSERT INTO Positions(Value) values('Project Manager');
                INSERT INTO Positions(Value) values('PMO Manager');
                INSERT INTO Positions(Value) values('Project portfolio manager');
                INSERT INTO Positions(Value) values('Head of company');
            ");

            Sql(@"
                INSERT INTO Sections(Name, TemplateId) values('Sensing', 1);
                INSERT INTO Sections(Name, TemplateId) values('Learning', 1);
                INSERT INTO Sections(Name, TemplateId) values('Renewal and replication', 1);
                INSERT INTO Sections(Name, TemplateId) values('Seizing / utilization', 1);
                INSERT INTO Sections(Name, TemplateId) values('Risk identification and classification', 1);
            ");

            Sql(@"
                INSERT INTO Questions(Value, SectionId) values('We frequently look for new business opportunities', 1);
                INSERT INTO Questions(Value, SectionId) values('We regularly review the effects of change in business environment on customers', 1);
                INSERT INTO Questions(Value, SectionId) values('We continuously monitor the environment for changes in customer needs, competitors and technology', 1);
                INSERT INTO Questions(Value, SectionId) values('We ensure our new products are in accordance with the customer requirements', 1);

                INSERT INTO Questions(Value, SectionId) values('We have effective routines to identify and use new information and knowledge', 2);
                INSERT INTO Questions(Value, SectionId) values('We have adequate routines to assimilate/collect new information and knowledge', 2);
                INSERT INTO Questions(Value, SectionId) values('We effectively transform existing information into new knowledge', 2);
                INSERT INTO Questions(Value, SectionId) values('We effectively develop usable new knowledge', 2);

                INSERT INTO Questions(Value, SectionId) values('We effectively transform existing processes to meet requirement for new products', 3);
                INSERT INTO Questions(Value, SectionId) values('We effectively adopt alternative processes or methods', 3);
                INSERT INTO Questions(Value, SectionId) values('We efficiently implement processes and methods to make new products', 3);

                INSERT INTO Questions(Value, SectionId) values('We devote a lot of time to implement the ideas for new products or process and improving existing ones', 4);
                INSERT INTO Questions(Value, SectionId) values('We quickly implement new process for improving product', 4);
                INSERT INTO Questions(Value, SectionId) values('We effectively utilize the knowledge obtained to improve old products or make new products', 4);
                INSERT INTO Questions(Value, SectionId) values('We are effectively able to respond to the identified business opportunities', 4);

                INSERT INTO Questions(Value, SectionId) values('We give high priority to identifying portfolio risks', 5);
                INSERT INTO Questions(Value, SectionId) values('We have specific tools and methods designed for identification of portfolio risks', 5);
                INSERT INTO Questions(Value, SectionId) values('We periodically asses for new portfolio risks', 5);
                INSERT INTO Questions(Value, SectionId) values('We have efficient methods to classify the identified risk', 5);
            ");

            Sql(@"
                INSERT INTO LikertItems(Value) values('Strongly disagree');
                INSERT INTO LikertItems(Value) values('Disagree');
                INSERT INTO LikertItems(Value) values('Neither agree nor disagree');
                INSERT INTO LikertItems(Value) values('Agree');
                INSERT INTO LikertItems(Value) values('Strongly agree');
            ");

            Sql(@"
                INSERT INTO Templates_CompanySizes(TemplateRefId, CompanySizeRefId) values(1, 1);
                INSERT INTO Templates_CompanySizes(TemplateRefId, CompanySizeRefId) values(1, 2);
                INSERT INTO Templates_CompanySizes(TemplateRefId, CompanySizeRefId) values(1, 3);
                INSERT INTO Templates_CompanySizes(TemplateRefId, CompanySizeRefId) values(1, 4);
            ");

            Sql(@"
                INSERT INTO Templates_LikertItems(TemplateRefId, LikertItemRefId, OrderNumber) values(1, 1, 1);
                INSERT INTO Templates_LikertItems(TemplateRefId, LikertItemRefId, OrderNumber) values(1, 2, 2);
                INSERT INTO Templates_LikertItems(TemplateRefId, LikertItemRefId, OrderNumber) values(1, 3, 3);
                INSERT INTO Templates_LikertItems(TemplateRefId, LikertItemRefId, OrderNumber) values(1, 4, 4);
                INSERT INTO Templates_LikertItems(TemplateRefId, LikertItemRefId, OrderNumber) values(1, 5, 5);
            ");

            Sql(@"
                INSERT INTO Templates_Positions(TemplateRefId, PositionRefId) values(1, 1);
                INSERT INTO Templates_Positions(TemplateRefId, PositionRefId) values(1, 2);
                INSERT INTO Templates_Positions(TemplateRefId, PositionRefId) values(1, 3);
                INSERT INTO Templates_Positions(TemplateRefId, PositionRefId) values(1, 4);
            ");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Templates", new[] { "LastUpdateUserId" });
            DropIndex("dbo.Templates", new[] { "CreationUserId" });
            AlterColumn("dbo.Templates", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Templates", "CreationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Templates", "LastUpdateUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Templates", "CreationUserId", c => c.String(nullable: false, maxLength: 128));
            RenameIndex(table: "dbo.Templates_LikertItems", name: "IX_LikertItemRefId", newName: "IX_LikertItemId");
            RenameIndex(table: "dbo.Templates_LikertItems", name: "IX_TemplateRefId", newName: "IX_TemplateId");
            RenameColumn(table: "dbo.Templates_LikertItems", name: "TemplateRefId", newName: "TemplateId");
            RenameColumn(table: "dbo.Templates_LikertItems", name: "LikertItemRefId", newName: "LikertItemId");
            CreateIndex("dbo.Templates", "LastUpdateUserId");
            CreateIndex("dbo.Templates", "CreationUserId");
        }
    }
}
