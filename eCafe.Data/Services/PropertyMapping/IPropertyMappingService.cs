using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Services.PropertyMapping
{
    public interface IPropertyMappingService
    {
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);

        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    }
}
