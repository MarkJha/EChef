using eCafe.Infrastructure.Common;
using eCafe.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCafe.WebUI.Common
{
    public static class ResourceUri<T> where T : class
    {
        private static string CreateResourceUri(ResourceParameter parameters, ResourceUriType type, IUrlHelper urlHelper, string urlName)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return urlHelper.Link(urlName,
                      new
                      {
                          fields = parameters.Fields,
                          orderBy = parameters.OrderBy,
                          searchQuery = parameters.SearchQuery,
                          genre = parameters.Genre,
                          pageNumber = parameters.PageNumber - 1,
                          pageSize = parameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return urlHelper.Link(urlName,
                      new
                      {
                          fields = parameters.Fields,
                          orderBy = parameters.OrderBy,
                          searchQuery = parameters.SearchQuery,
                          genre = parameters.Genre,
                          pageNumber = parameters.PageNumber + 1,
                          pageSize = parameters.PageSize
                      });

                default:
                    return urlHelper.Link(urlName,
                    new
                    {
                        fields = parameters.Fields,
                        orderBy = parameters.OrderBy,
                        searchQuery = parameters.SearchQuery,
                        genre = parameters.Genre,
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize
                    });
            }
        }

        public static object CreatePaginationHeader(ResourceParameter parameters, PagedList<T> results, IUrlHelper urlHelper, string urlName)
        {
            var previousPageLink = results.HasPrevious ?
                CreateResourceUri(parameters,
                ResourceUriType.PreviousPage, urlHelper, urlName) : null;

            var nextPageLink = results.HasNext ?
                CreateResourceUri(parameters,
                ResourceUriType.NextPage, urlHelper, urlName) : null;

            var paginationMetadata = new
            {
                totalCount = results.TotalCount,
                pageSize = results.PageSize,
                currentPage = results.CurrentPage,
                totalPages = results.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            return paginationMetadata;
        }
    }
}
