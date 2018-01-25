﻿using eCafe.Core.Entities;
using eCafe.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCafe.WebUI.Services.ApiGetAll
{   
    public interface IMenuGet
    {
        PagedList<Menu> GetAll(ResourceParameter resourceParameter);
    }
}
