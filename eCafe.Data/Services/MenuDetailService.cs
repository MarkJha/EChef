using eCafe.Infrastructure.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using eCafe.Infrastructure.Common;
using eCafe.Infrastructure.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Mapper;
using eCafe.Core.Entities;
using System.Linq;

namespace eCafe.Infrastructure.Services
{
    public class MenuDetailService : IMenuDetailService
    {
        private readonly IMenuDetailRepository _menuDetailRepository;
        private readonly IMenuImageRepository _menuImageRepository;

        public MenuDetailService(IMenuDetailRepository menuDetailRepository, IMenuImageRepository menuImageRepository)
        {
            _menuDetailRepository = menuDetailRepository;
            _menuImageRepository = menuImageRepository;
        }

        public async Task<MenuDetailDto> CreateMenuDetailAsync(MenuDetailDto model)
        {
            var entity = await _menuDetailRepository
                         .InsertAsync(model.ToEntity());

            //*** saving image into DB
            foreach (var item in model.ImageDetails)
            {
                item.MenuDetailId = entity.Id;
                await _menuImageRepository.InsertAsync(item.ToEntity());
            }

            return entity.ToViewModel();
        }

        public async Task<MenuDetailDto> DeleteMenuDetailAsync(int id)
        {
            var entity = await _menuDetailRepository.GetSingleAsync(id);

            var isDeleted = await _menuImageRepository
                                        .DeleteMenuImageDetailAsync(id);

            entity.Id = isDeleted != 0 ? entity.Id : 0;
            return entity.ToViewModel();
        }

        public async Task<IEnumerable<MenuDetailDto>> GetMenuDetailAsync(int pageSize = 10, int pageNumber = 1, string name = null)
        {
            var model = await _menuDetailRepository
                                .AllIncludingAsync(m => m.Menu, x => x.ImageDetails);

            return model
                        .OrderByDescending(a => a.Id)
                        .Skip(pageNumber - 1 * pageSize)
                        .Take(pageSize)
                        .Select(item => item.ToViewModel())
                        .ToList();
        }

        public async Task<MenuDetailDto> GetMenuDetailAsync(int id)
        {
            var entity = await _menuDetailRepository
                               .GetAsync(new MenuDetail { Id = id });

            return entity.ToViewModel();
        }

        public MenuDetailDto GetMenuDetail(int id)
        {
            var entity = _menuDetailRepository
                          .GetSingle(x => x.Id.Equals(id), y => y.Menu, z => z.ImageDetails, p => p.Menu.MainMenu);

            return entity.ToViewModel();
        }

        //public MenuDetailDto GetAllCategory()
        //{
        //    var entity = _menuDetailRepository
        //                  .GetAll()
        //                  .GroupBy(x=>x.Menu.Name)
        //                  .Select(n => new
        //                  {
        //                      CategoryName = n.Key,
        //                      Count = n.Count()
        //                  })
        //                  .OrderBy(n => n.CategoryName)
        //                  .ToList();

        //    return entity.ToViewModel();
        //}

        public Task<IEnumerable<MenuDetailDto>> GetMenuDetailAsyncV2(ResourceParameter parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsMenuDetailDuplicateAsync(MenuDetailDto model)
            => await _menuDetailRepository.IsExistsAsync(x => x.Name.Equals(model.Name));

        public async Task<MenuDetailDto> UpdateMenuDetailAsync(MenuDetailDto model)
        {
            var entity = await _menuDetailRepository
                             .UpdateAsync(model.ToEntity());

            return entity != null ? entity.ToViewModel() : new MenuDetailDto();
        }

        public Task<MenuDetailDto> UpdateMenuPartiallyAsync(int id, JsonPatchDocument<MenuDetailDto> menuPatch)
        {
            throw new NotImplementedException();
        }

        public void UploadPictures()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve all program list by passing program name
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MenuDetailDto>> SearchByMenuName(string menuName)
                        => await _menuDetailRepository.GetAllMenusBySearchKey(menuName);

        public async Task<IEnumerable<MenuDetailDto>> GetMenuDetailByAsync(int id)
        {
            var model = await _menuDetailRepository
                                .FindByAsync(x=>x.MenuId.Equals(id));

            return model
                        .OrderByDescending(a => a.Id)                        
                        .Select(item => item.ToViewModel())
                        .ToList();
        }
    }
}
