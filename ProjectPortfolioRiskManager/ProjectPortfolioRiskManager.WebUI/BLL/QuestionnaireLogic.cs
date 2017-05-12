using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPortfolioRiskManager.WebUI.BLL
{
    public static class QuestionnaireLogic
    {
        private const string companySizesTag = @"<div class=""companySize-block"">";
        private const string companySizesPartialName = "CompanySizePartial";
        private const string positionsTag = @"<div class=""position-block"">";
        private const string positionsPartialName = "PositionPartial";
        private const string dynamicCapabilitiesTag = @"<table class=""table table-bordered dynamicCapabilities-block"">";
        private const string sectionPartialName = "SectionPartial";
        private const string portfolioRiskManagementTag = @"<table class=""table table-bordered portfolioRiskManagement-block"">";
        private const string divEndTag = "</div>";
        private const string tableEndTag = "</table>";

        public static string PopulateContent(int templateId, string content, Func<string, object, string> renderView, ICompanySizeRepository companySizeRepository,
            IPositionRepository positionRepository, ISectionRepository sectionRepository, IQuestionRepository questionRepository, ILikertItemRepository likertItemRepository)
        {
            IEnumerable<CompanySize> companySizes = companySizeRepository.GetByTemplate(templateId);
            IEnumerable<Position> positions = positionRepository.GetByTemplate(templateId);
            var dynamicCapabilities = new SectionViewModel(templateId, sectionRepository, questionRepository, likertItemRepository);
            var portfolioRiskManagement = new SectionViewModel(templateId, sectionRepository, questionRepository, likertItemRepository, true);

            string result = Populate(companySizes, renderView, companySizesPartialName, companySizesTag, content);
            result = Populate(positions, renderView, positionsPartialName, positionsTag, result);
            result = Populate(dynamicCapabilities, renderView, sectionPartialName, dynamicCapabilitiesTag, result);
            result = Populate(portfolioRiskManagement, renderView, sectionPartialName, portfolioRiskManagementTag, result);
            return result;
        }

        public static string ProcessTemplate()
        {

        }

        private static string Populate<T>(T model, Func<string, object, string> renderView, string partialName, string tag, string current)
            where T : class
        {
            string html = renderView(partialName, model);
            int index = current.IndexOf(tag) + tag.Length;
            return current.Insert(index, html);
        }

        public static void ProcessCompanySizes(string html)
        {

        }
    }
}