using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.WebUI.Models;
using ProjectPortfolioRiskManager.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectPortfolioRiskManager.WebUI.BLL
{
    public static class TemplateLogic
    {
        public static string GetFullContent(int templateId, string content, Func<string, object, string> renderPartialView, ICompanySizeRepository companySizeRepository,
            IPositionRepository positionRepository, ISectionRepository sectionRepository, IQuestionRepository questionRepository, ILikertItemRepository likertItemRepository)
        {
            IEnumerable<CompanySize> companySizes = companySizeRepository.GetByTemplate(templateId);
            IEnumerable<Position> positions = positionRepository.GetByTemplate(templateId);
            var dynamicCapabilities = new SectionViewModel(templateId, sectionRepository, questionRepository, likertItemRepository);
            var portfolioRiskManagement = new SectionViewModel(templateId, sectionRepository, questionRepository, likertItemRepository, true);

            string result = GetRenderedPartial(companySizes, renderPartialView, ParsingTags.CompanySizesPartialName, ParsingTags.CompanySizesBlockStartTag, content);
            result = GetRenderedPartial(positions, renderPartialView, ParsingTags.PositionsPartialName, ParsingTags.PositionsBlockStartTag, result);
            result = GetRenderedPartial(dynamicCapabilities, renderPartialView, ParsingTags.DynamicCapabilitiesPartialName, ParsingTags.DynamicCapabilitiesBlockStartTag, result);
            result = GetRenderedPartial(portfolioRiskManagement, renderPartialView, ParsingTags.PortfolioRiskManagementPartialName, ParsingTags.PortfolioRiskManagementBlockStartTag, result);
            return result;
        }

        public static string ProcessContent(string content, bool isToInsert, out List<string> companySizes, out List<string> positions, out List<string> sections, out List<string> questions,
            out List<string> likertItems, out List<int> questionsToSectionsMapping)
        {
            questionsToSectionsMapping = new List<int>();
            var regex = new Regex(string.Join("", ParsingTags.CompanySizesBlockStartTag, "(.*?)", ParsingTags.CompanySizesBlockEndTag), RegexOptions.Singleline);
            Match match = regex.Match(content);
            string contentToProcess = match.Groups[1].Value;
            companySizes = ExtractList(contentToProcess, ParsingTags.CompanySizesElementStartTag, ParsingTags.CompanySizesElementEndTag);
            var result = content.Replace(contentToProcess, string.Empty);

            regex = new Regex(string.Join("", ParsingTags.PositionsBlockStartTag, "(.*?)", ParsingTags.PositionsBlockEndTag), RegexOptions.Singleline);
            match = regex.Match(result);
            contentToProcess = match.Groups[1].Value;
            positions = ExtractList(contentToProcess, ParsingTags.PositionsElementStartTag, ParsingTags.PositionsElementEndTag);
            result = result.Replace(contentToProcess, string.Empty);

            regex = new Regex(string.Join("", ParsingTags.DynamicCapabilitiesBlockStartTag, "(.*?)", ParsingTags.DynamicCapabilitiesBlockEndTag), RegexOptions.Singleline);
            match = regex.Match(result);
            contentToProcess = match.Groups[1].Value;
            sections = ExtractList(contentToProcess, ParsingTags.SectionsElementStartTag, ParsingTags.SectionsElementEndTag);
            questions = ExtractList(contentToProcess, ParsingTags.QuestionsElementStartTag, ParsingTags.QuestionsElementEndTag);
            regex = new Regex(string.Join("", ParsingTags.LikertItemsBlockStartTag, "(.*?)", ParsingTags.LikertItemsBlockEndTag), RegexOptions.Singleline);
            match = regex.Match(result);
            string likertItemsBlock = match.Groups[1].Value;
            likertItems = ExtractList(likertItemsBlock, ParsingTags.LikertItemsElementStartTag, ParsingTags.LikertItemsElementEndTag);
            if (isToInsert)
            {
                MapQuestionsToSections(contentToProcess, questionsToSectionsMapping);
            }
            result = result.Replace(contentToProcess, string.Empty);

            regex = new Regex(string.Join("", ParsingTags.PortfolioRiskManagementBlockStartTag, "(.*?)", ParsingTags.PortfolioRiskManagementBlockEndTag), RegexOptions.Singleline);
            match = regex.Match(result);
            contentToProcess = match.Groups[1].Value;
            sections.AddRange(ExtractList(contentToProcess, ParsingTags.SectionsElementStartTag, ParsingTags.SectionsElementEndTag));
            questions.AddRange(ExtractList(contentToProcess, ParsingTags.QuestionsElementStartTag, ParsingTags.QuestionsElementEndTag));
            if (isToInsert)
            {
                MapQuestionsToSections(contentToProcess, questionsToSectionsMapping);
            }
            result = result.Replace(contentToProcess, string.Empty);
            return result;
        }

        private static string GetRenderedPartial<T>(T model, Func<string, object, string> renderView, string partialName, string tag, string current)
            where T : class
        {
            string html = renderView(partialName, model);
            int index = current.IndexOf(tag) + tag.Length;
            return current.Insert(index, html);
        }

        private static List<string> ExtractList(string html, string leftTag, string rightTag)
        {
            var regex = new Regex(string.Join("", leftTag, "(.*?)(", rightTag, "|$)"), RegexOptions.Singleline);
            MatchCollection matchList = regex.Matches(html);
            List<string> list = matchList.Cast<Match>().Select(x => x.Groups[1].Value.Trim()).ToList();
            return list;
        }

        private static void MapQuestionsToSections(string contentToProcess, List<int> questionsToSectionsMapping)
        {
            var regex = new Regex(string.Join("", ParsingTags.SectionsBlockStartTag, "(.*?)", ParsingTags.SectionsBlockEndTag), RegexOptions.Singleline);
            MatchCollection matchList = regex.Matches(contentToProcess);
            List<string> list = matchList.Cast<Match>().Select(x => x.Groups[1].Value.Trim()).ToList();
            regex = new Regex(string.Join("", ParsingTags.QuestionsElementStartTag, "(.*?)", ParsingTags.QuestionsElementEndTag), RegexOptions.Singleline);

            foreach (var item in list)
            {
                matchList = regex.Matches(item);
                questionsToSectionsMapping.Add(matchList.Count);
            }
        }
    }
}