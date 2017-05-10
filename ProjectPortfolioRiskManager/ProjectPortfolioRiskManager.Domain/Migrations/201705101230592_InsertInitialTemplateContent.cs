namespace ProjectPortfolioRiskManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertInitialTemplateContent : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                UPDATE Templates
                SET Content =  '<p>Questionnaire<br />Likert scale will be used for assessment: Strongly agree &rarr; Strongly Disagree</p>
                                <div>
	                                Size of company:
	                                <div class=""companySize-block""></div>
                                </div>
                                <br />
                                <div>
	                                Role/position:
	                                <div class=""position-block""></div>
                                </div>
                                <br />
                                <div class=""row control-group"">
	                                <div class=""form-group col-xs-4 floating-label-form-group controls"">
		                                <label>Industry</label>
		                                <input type=""text"" name=""Industry"" class=""form-control"" placeholder=""Industry"" />
	                                </div>
                                </div>
                                <br />
                                <div>
	                                Dynamic capabilities:
	                                <table class=""table table-bordered dynamicCapabilities-block""></table>
                                </div>
                                <div>
	                                Portfolio risk management:
	                                <table class=""table table-bordered portfolioRiskManagement-block""></table>
                                </div>';
            ");
        }
        
        public override void Down()
        {
        }
    }
}
