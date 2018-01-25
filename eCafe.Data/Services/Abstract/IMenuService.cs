using eCafe.Infrastructure.Common;
using eCafe.Infrastructure.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Services.Abstract
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDto>> GetMenuAsync(int pageSize = 10, int pageNumber = 1, string name = null);

        Task<IEnumerable<MenuDto>> GetMenuAsyncV2(ResourceParameter parameters);

        Task<MenuDto> GetMenuAsync(int id);

        Task<MenuDto> CreateMenuAsync(MenuDto model);

        Task<MenuDto> UpdateMenuAsync(MenuDto model);

        Task<MenuDto> UpdateMenuPartiallyAsync(int id, JsonPatchDocument<MenuDto> menuPatch);

        Task<MenuDto> DeleteMenuAsync(int id);

        Task<bool> IsMenuDuplicateAsync(MenuDto model);

        IEnumerable<SelectOption> GetSelectOption(int id);

        Task<IEnumerable<SubMenuResultSet>> GetSubMenuWithSubMenuDetailCount();
    }
}
