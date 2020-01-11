using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Admin.Models;

namespace MrCMS.Web.Apps.Admin.Services
{
    public interface IMergeWebpageAdminService
    {
        IEnumerable<SelectListItem> GetValidParents(Webpage webpage);
        MergeWebpageResult Validate(MergeWebpageModel moveWebpageModel);
        Task<MergeWebpageConfirmationModel> GetConfirmationModel(MergeWebpageModel model);
        Task<MergeWebpageResult> Confirm(MergeWebpageModel model);
        MergeWebpageModel GetModel(Webpage webpage);
    }
}