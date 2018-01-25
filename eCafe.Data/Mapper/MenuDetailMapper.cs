using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCafe.Infrastructure.Mapper
{
    public static class MenuDetailMapper
    {
        public static MenuDetailDto ToViewModel(this MenuDetail entity)
        {
            return entity == null ? null : new MenuDetailDto
            {
                Id = entity.Id,
                MenuId = entity.MenuId,
                MainMenuId = entity.Menu != null ? entity.Menu.MainMenuId : 0,
                MainMenuName = entity.Menu?.MainMenu?.Name,
                MenuName = entity.Menu?.Name,
                Name = entity.Name,
                Description = entity.Description,
                Ingredients = entity.Ingredients,
                Receipe = entity.Receipe,
                Discount = entity.Discount,
                IsSpecial = entity.IsSpecial,
                IsVeg = entity.IsVeg,
                Quantity = entity.Quantity,
                Rate = entity.Rate,
                ServePeoples = entity.ServePeoples,
                Guid = entity.Guid,
                IsActive = entity.IsActive,
                ImagePath = entity.ImagePath,
                ImageDetails = (from imagedetails in entity?.ImageDetails
                                select new MenuImageDetailDto
                                {
                                    ImageDesc = imagedetails?.ImageDesc,
                                    ImagePath = imagedetails?.ImagePath,
                                    MenuDetailId = imagedetails.Id
                                }).ToList()
            };
        }



        public static MenuDetail ToEntity(this MenuDetailDto viewModel)
        {
            return viewModel == null ? null : new MenuDetail
            {
                Id = viewModel.Id,
                MenuId = viewModel.MenuId,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Ingredients = viewModel.Ingredients,
                Receipe = viewModel.Receipe,
                Discount = viewModel.Discount,
                IsSpecial = viewModel.IsSpecial,
                IsVeg = viewModel.IsVeg,
                Quantity = viewModel.Quantity,
                Rate = viewModel.Rate,
                ServePeoples = viewModel.ServePeoples,
                Guid = viewModel.Guid,
                IsActive = viewModel.IsActive,
                ImagePath = viewModel.ImagePath
            };
        }
    }
}
