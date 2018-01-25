using eCafe.Core.Entities;
using eCafe.Infrastructure.Common;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Extensions;
using eCafe.Infrastructure.Models;
using eCafe.Infrastructure.Services.PropertyMapping;
using System.Linq;

namespace eCafe.WebUI.Services.ApiGetAll
{
    public class MenuGet : IMenuGet
    {
        private readonly ECafeContext _context;
        private IPropertyMappingService _propertyMappingService;

        public MenuGet(ECafeContext context,
            IPropertyMappingService propertyMappingService)
        {
            this._context = context;
            _propertyMappingService = propertyMappingService;
        }

        public PagedList<Menu> GetAll(ResourceParameter resourceParameter)
        {
            var collectionBeforePaging =
                _context.Menus.ApplySort(resourceParameter.OrderBy,
                _propertyMappingService.GetPropertyMapping<MenuDto, Menu>());

            if (!string.IsNullOrEmpty(resourceParameter.Genre))
            {
                // trim & ignore casing
                var genreForWhereClause = resourceParameter.Genre
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Name.ToLowerInvariant() == genreForWhereClause);
            }

            if (!string.IsNullOrEmpty(resourceParameter.SearchQuery))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = resourceParameter.SearchQuery.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Name.ToLowerInvariant().Contains(searchQueryForWhereClause)
                    || a.Description.ToLowerInvariant().Contains(searchQueryForWhereClause));
            }

            return PagedList<Menu>
                    .Create(collectionBeforePaging, resourceParameter.PageNumber, resourceParameter.PageSize);
        }
    }
}
