using eCafe.Infrastructure.Services.Abstract;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using eCafe.Infrastructure.Mapper;
using eCafe.Infrastructure.Common;
using Microsoft.AspNetCore.JsonPatch;
using eCafe.Core.Entities;
using eCafe.Infrastructure.Repository.Interface;

namespace eCafe.Infrastructure.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<MenuDto> CreateMenuAsync(MenuDto model)
        {
            var entity = await _menuRepository
                          .InsertAsync(model.ToEntity());

            return entity.ToViewModel();
        }

        public async Task<MenuDto> DeleteMenuAsync(int id)
        {
            var entity = await _menuRepository.GetAsync(id);
            var isDeleted = await _menuRepository.DeleteMenuAsync(id);
            entity.Id = isDeleted != 0 ? entity.Id : 0;
            return entity.ToViewModel();
        }

        public async Task<IEnumerable<MenuDto>> GetMenuAsync(int pageSize = 10, int pageNumber = 1, string name = null)
        {
            var model = await _menuRepository
                           .AllIncludingAsync(m => m.MainMenu);

            return model
                        .OrderBy(a => a.Id)
                        .Skip(pageNumber - 1 * pageSize)
                        .Take(pageSize)
                        .Select(item => item.ToViewModel())
                        .ToList();
        }


        public async Task<IEnumerable<SubMenuResultSet>> GetSubMenuWithSubMenuDetailCount()
        {
            var subMenu = await _menuRepository.GetAllAsync();

            var subMenuDetails = await _menuRepository
                           .AllIncludingAsync(m => m.SubMenuDetails);

            // var subMenuDetails = model.Select(x => x.SubMenuDetails).ToList();

            //var result = subMenu
            //            .GroupJoin(subMenuDetails, detail => detail.Id,
            //             menu => menu.Id, (subMenuDetail, menu)
            //        => new SubMenuResultSet
            //        {
            //            Id = subMenuDetail.Id,
            //            Name = subMenuDetail.Name,
            //            Count = subMenu.Count()
            //        })
            //    .ToList();

            var result = subMenu
                       .GroupJoin(subMenuDetails, detail => detail.Id,
                         menu => menu.Id, (subMenuDetail, menu)
                => new
                {
                    Key = subMenuDetail,
                    Category = menu
                })
            .ToList();

            var categoryResult = new List<SubMenuResultSet>();

            foreach (var item in result)
            {
                foreach (var cat in item.Category)
                {
                    var category = new SubMenuResultSet
                    {
                        Id = cat.Id,
                        Name = cat.Name,
                        Count = cat.SubMenuDetails.Count()
                    };
                    categoryResult.Add(category);
                }                
            }

            return categoryResult;
        }

        public async Task<MenuDto> GetMenuAsync(int id)
        {
            var entity = await _menuRepository
                               .GetAsync(new Menu { Id = id });

            return entity.ToViewModel();
        }

        public Task<IEnumerable<MenuDto>> GetMenuAsyncV2(ResourceParameter parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectOption> GetSelectOption(int id)
        {
            return _menuRepository
                               .GetAll()
                               .Where(x => x.MainMenuId.Equals(id))
                               .OrderBy(x => x.Name)
                               .Select(menu => new SelectOption
                               {
                                   Value = menu.Name,
                                   Id = menu.Id
                               }).ToList();
        }

        public async Task<bool> IsMenuDuplicateAsync(MenuDto model)
            => await _menuRepository.IsExistsAsync(x => x.Name.Equals(model.Name));

        public async Task<MenuDto> UpdateMenuAsync(MenuDto model)
        {
            var entity = await _menuRepository
                            .UpdateAsync(model.ToEntity());

            return entity != null ? entity.ToViewModel() : new MenuDto();
        }

        public Task<MenuDto> UpdateMenuPartiallyAsync(int id, JsonPatchDocument<MenuDto> menuPatch)
        {
            throw new NotImplementedException();
        }
    }
}

