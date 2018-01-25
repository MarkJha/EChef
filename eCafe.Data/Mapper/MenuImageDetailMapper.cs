using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Mapper
{
    public static class MenuImageDetailMapper
    {
        public static MenuImageDetailDto ToViewModel(this SubMenuImageDetail entity)
        {
            return entity == null ? null : new MenuImageDetailDto
            {
               ImageDesc=entity.ImageDesc,
               ImagePath=entity.ImagePath,
               MenuDetailId=entity.MenuDetailId
            };
        }

        public static SubMenuImageDetail ToEntity(this MenuImageDetailDto viewModel)
        {
            return viewModel == null ? null : new SubMenuImageDetail
            {
               ImageDesc=viewModel.ImageDesc,
               ImagePath=viewModel.ImagePath,
               MenuDetailId=viewModel.MenuDetailId
            };
        }
    }
}
