using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;

namespace eCafe.Infrastructure.Mapper
{
    public static class MainMenuMapper
    {
        /// <summary>
        /// Convert db entity to view model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static MainMenuDto ToViewModel(this MainMenu entity)
        {
            return entity == null ? null : new MainMenuDto
            {
                Id = entity.Id,
                Name=entity.Name,
                Description=entity.Description,
                ImagePath=entity.ImagePath,
                Guid = entity.Guid,
                IsActive = entity.IsActive
            };
        }

        /// <summary>
        /// Convert view model to db entity
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static MainMenu ToEntity(this MainMenuDto viewModel)
        {
            return viewModel == null ? null : new MainMenu
            {
                Id=viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                ImagePath=viewModel.ImagePath,
                Guid = viewModel.Guid,
                IsActive=viewModel.IsActive                
            };
        }
    }
}
