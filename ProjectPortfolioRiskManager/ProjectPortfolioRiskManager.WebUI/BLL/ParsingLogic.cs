using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProjectPortfolioRiskManager.WebUI.BLL
{
    public static class ParsingLogic
    {
        public static string ExtractList(string html, string leftTag, string rightTag)
        {
            var regex = new Regex(string.Join("", leftTag, "(.*?)(", rightTag, "|$)"));
            var matchList = regex.Matches(html);
            var list = matchList.Cast<Match>().Select(match => match.Groups[1].Value.Trim()).ToList();
            return null;
        }
    }
}