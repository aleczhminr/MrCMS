using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MrCMS.Data;
using MrCMS.Entities.Documents;
using MrCMS.Entities.Documents.Web;
using MrCMS.Helpers;
using MrCMS.Services;
using MrCMS.Web.Areas.Admin.Models;

namespace MrCMS.Web.Areas.Admin.Services
{
    public class DefaultWebpageTreeNavListing : IWebpageTreeNavListing
    {
        private readonly ITreeNavService _treeNavService;
        private readonly IUrlHelper _urlHelper;
        private readonly IValidWebpageChildrenService _validWebpageChildrenService;
        private readonly IRepository<Webpage> _repository;

        public DefaultWebpageTreeNavListing(IValidWebpageChildrenService validWebpageChildrenService, IRepository<Webpage> repository,
            IUrlHelper urlHelper, ITreeNavService treeNavService)
        {
            _validWebpageChildrenService = validWebpageChildrenService;
            _repository = repository;
            _urlHelper = urlHelper;
            _treeNavService = treeNavService;
        }

        public AdminTree GetTree(int? id)
        {
            Webpage parent = id.HasValue ? _repository.LoadSync(id.Value) : null;
            var adminTree = new AdminTree
            {
                RootContoller = "Webpage",
                IsRootRequest = parent == null
            };
            int maxChildNodes = parent?.GetMetadata().MaxChildNodes ?? 1000;
            var query = GetQuery(parent);

            int rowCount = GetRowCount(query);
            query.Take(maxChildNodes).ToList().ForEach(doc =>
            {
                DocumentMetadata documentMetadata = doc.GetMetadata();
                var node = new AdminTreeNode
                {
                    Id = doc.Id,
                    ParentId = doc.ParentId,
                    Name = doc.Name,
                    IconClass = documentMetadata.IconClass,
                    NodeType = "Webpage",
                    Type = documentMetadata.Type.FullName,
                    HasChildren = _treeNavService.WebpageHasChildren(doc.Id),
                    Sortable = documentMetadata.Sortable,
                    CanAddChild = _validWebpageChildrenService.AnyValidWebpageDocumentTypes(doc),
                    IsPublished = doc.Published,
                    RevealInNavigation = doc.RevealInNavigation,
                    Url = _urlHelper.Action("Edit", "Webpage", new { id = doc.Id })
                };
                adminTree.Nodes.Add(node);
            });
            if (rowCount > maxChildNodes)
            {
                adminTree.Nodes.Add(new AdminTreeNode
                {
                    IconClass = "fa fa-plus",
                    IsMoreLink = true,
                    ParentId = id,
                    Name = (rowCount - maxChildNodes) + " More",
                    Url = _urlHelper.Action("Search", "WebpageSearch", new { parentId = id }),
                });
            }
            return adminTree;
        }

        public bool HasChildren(int id)
        {
            var parent = _repository.LoadSync(id);
            var query = GetQuery(parent);
            return GetRowCount(query) > 0;
        }

        private static int GetRowCount(IQueryable< Webpage> query)
        {
            return query.Count();
        }

        private IQueryable<Webpage> GetQuery(Webpage parent)
        {
            var query = _repository.Query();
            if (parent != null)
            {
                query = query.Where(x => x.ParentId == parent.Id);
                DocumentMetadata metaData = parent.GetMetadata();
                query = ApplySort(metaData, query);
            }
            else
            {
                query = query.Where(x => x.ParentId == null);
                query = query.OrderBy(x => x.DisplayOrder);
            }
            return query;
        }

        private static IQueryable<Webpage> ApplySort(DocumentMetadata metaData,
            IQueryable<Webpage> query)
        {
            query = metaData.SortBy switch
            {
                SortBy.DisplayOrder => query.OrderBy(webpage => webpage.DisplayOrder),
                SortBy.DisplayOrderDesc => query.OrderByDescending(webpage => webpage.DisplayOrder),
                SortBy.PublishedOn => query.OrderByDescending(x => x.Published).ThenBy(webpage => webpage.PublishOn),
                SortBy.PublishedOnDesc => query.OrderByDescending(x => x.Published)
                    .ThenByDescending(webpage => webpage.PublishOn),
                SortBy.CreatedOn => query.OrderBy(webpage => webpage.CreatedOn),
                SortBy.CreatedOnDesc => query.OrderByDescending(webpage => webpage.CreatedOn),
                _ => throw new ArgumentOutOfRangeException()
            };
            return query;
        }
    }
}