using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Admin.Models.SEOAnalysis;

namespace MrCMS.Web.Apps.Admin.Services.SEOAnalysis
{
    public class BodyContentChecks : BaseSEOAnalysisFacetProvider
    {
        public override async IAsyncEnumerable<SEOAnalysisFacet> GetFacets(Webpage webpage, HtmlNode document,
            string analysisTerm)
        {
            var text =
                (HtmlNode.CreateNode($"<div>{webpage.BodyContent}</div>").InnerText ?? string.Empty).Replace(
                    Environment.NewLine, " ");

            if (text.Contains(analysisTerm, StringComparison.OrdinalIgnoreCase))
            {
                yield return
                    await GetFacet("Body Content", SEOAnalysisStatus.Success,
                        string.Format("Body content contains '{0}'", analysisTerm));
            }
            else
            {
                yield return
                    await GetFacet("Body Content", SEOAnalysisStatus.Error,
                        string.Format("Body content does not contain '{0}'", analysisTerm));
            }
        }
    }
}