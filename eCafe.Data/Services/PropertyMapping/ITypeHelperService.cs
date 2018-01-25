using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Services.PropertyMapping
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
