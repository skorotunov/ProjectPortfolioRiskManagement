namespace ProjectPortfolioRiskManager.WebUI.Utils
{
    public static class ParsingTags
    {
        public static string CompanySizesBlockStartTag = @"<div class=""companySize-block"">";
        public static string CompanySizesBlockEndTag = "</div>";
        public static string CompanySizesElementStartTag = @"<input name=""CompanySizeId"" type=""radio"" />";
        public static string CompanySizesElementEndTag = "<br />";
        public static string CompanySizesPartialName = "CompanySizePartial";

        public static string PositionsBlockStartTag = @"<div class=""position-block"">";
        public static string PositionsBlockEndTag = "</div>";
        public static string PositionsElementStartTag = @"<input name=""PositionId"" type=""radio"" />";
        public static string PositionsElementEndTag = "<br />";
        public static string PositionsPartialName = "PositionPartial";

        public static string DynamicCapabilitiesBlockStartTag = @"<table class=""table table-bordered dynamicCapabilities-block"">";
        public static string DynamicCapabilitiesBlockEndTag = "</table>";
        public static string DynamicCapabilitiesPartialName = "SectionPartial";

        public static string PortfolioRiskManagementBlockStartTag = @"<table class=""table table-bordered portfolioRiskManagement-block"">";
        public static string PortfolioRiskManagementBlockEndTag = "</table>";
        public static string PortfolioRiskManagementPartialName = "SectionPartial";

        public static string SectionsBlockStartTag = "<ol>";
        public static string SectionsBlockEndTag = "</ol>";
        public static string SectionsElementStartTag = @"<td class=""SectionName"">";
        public static string SectionsElementEndTag = "</td>";

        public static string QuestionsElementStartTag = "<li>";
        public static string QuestionsElementEndTag = "<br />";

        public static string LikertItemsBlockStartTag = @"<div class=""btn-group"" data-toggle=""buttons"">";
        public static string LikertItemsBlockEndTag = "</div>";
        public static string LikertItemsElementStartTag = @"type=""radio"" />";
        public static string LikertItemsElementEndTag = "</label>";
    }
}