using eCafe.Core.Entities;
using eCafe.Infrastructure.Common;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Extensions;
using eCafe.Infrastructure.Models;
using eCafe.Infrastructure.Services.PropertyMapping;
using System.Linq;

namespace eCafe.WebUI.Services.ApiGetAll
{
    public class MainMenuGet : IMainMenuGet
    {
        private readonly ECafeContext _context;
        private IPropertyMappingService _propertyMappingService;

        public MainMenuGet(ECafeContext context,
            IPropertyMappingService propertyMappingService)
        {
            this._context = context;
            _propertyMappingService = propertyMappingService;
        }

        public PagedList<MainMenu> GetAll(ResourceParameter resourceParameter)
        {
            var collectionBeforePaging =
                _context.MainMenus.ApplySort(resourceParameter.OrderBy,
                _propertyMappingService.GetPropertyMapping<MainMenuDto, MainMenu>());

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

            return PagedList<MainMenu>
                    .Create(collectionBeforePaging, resourceParameter.PageNumber, resourceParameter.PageSize);
        }
    }
}
