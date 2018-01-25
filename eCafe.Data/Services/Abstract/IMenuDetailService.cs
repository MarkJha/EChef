using eCafe.Infrastructure.Common;
using eCafe.Infrastructure.Mapper;
using eCafe.Infrastructure.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Services.Abstract
{
    public interface IMenuDetailService
    {
        Task<IEnumerable<MenuDetailDto>> GetMenuDetailAsync(int pageSize = 10, int pageNumber = 1, string name = null);

        Task<IEnumerable<MenuDetailDto>> GetMenuDetailAsyncV2(ResourceParameter parameters);

        Task<MenuDetailDto> GetMenuDetailAsync(int id);

        Task<IEnumerable<MenuDetailDto>> GetMenuDetailByAsync(int id);
        MenuDetailDto GetMenuDetail(int id);

        Task<MenuDetailDto> CreateMenuDetailAsync(MenuDetailDto model);

        Task<MenuDetailDto> UpdateMenuDetailAsync(MenuDetailDto model);

        Task<MenuDetailDto> UpdateMenuPartiallyAsync(int id, JsonPatchDocument<MenuDetailDto> menuPatch);

        Task<MenuDetailDto> DeleteMenuDetailAsync(int id);

        Task<bool> IsMenuDetailDuplicateAsync(MenuDetailDto model);

        void UploadPictures();

        Task<IEnumerable<MenuDetailDto>> SearchByMenuName(string menuName);
    }
}
