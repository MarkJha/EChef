using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;

namespace eCafe.Infrastructure.Mapper
{
    public static class MenuMapper
    {
        public static MenuDto ToViewModel(this Menu entity)
        {
            return entity == null ? null : new MenuDto
            {
                Id = entity.Id,
                MainMenuId = entity.MainMenuId,
                MenuName = entity.MainMenu?.Name,
                Name = entity.Name,
                Description = entity.Description,
                ImagePath = entity.ImagePath,
                Guid = entity.Guid,
                IsActive = entity.IsActive
            };
        }

        public static Menu ToEntity(this MenuDto viewModel)
        {
            return viewModel == null ? null : new Menu
            {
                Id = viewModel.Id,
                MainMenuId = viewModel.MainMenuId,
                Name = viewModel.Name,
                Description = viewModel.Description,
                ImagePath = viewModel.ImagePath,
                Guid = viewModel.Guid,
                IsActive = viewModel.IsActive
            };
        }
    }
}
