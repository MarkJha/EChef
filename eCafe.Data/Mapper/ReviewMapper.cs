using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Mapper
{
    public static class ReviewMapper
    {
        public static ReviewDto ToViewModel(this Review entity)
        {
            return entity == null ? null : new ReviewDto
            {
                Id = entity.Id,
                SubMenuId=entity.SubMenuDetailId,
                Comment = entity.Comment,
                Rating = entity.Rating,
                CustId = entity.CustomerId,
                Guid = entity.Guid,
                IsActive = entity.IsActive,
                MenuDetail = entity.SubMenuDetail.ToViewModel()
            };
        }

        /// <summary>
        /// Convert view model to db entity
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static Review ToEntity(this ReviewDto viewModel)
        {
            return viewModel == null ? null : new Review
            {
                Id = viewModel.Id,
                CustomerId=viewModel.CustId,
                SubMenuDetailId=viewModel.SubMenuId,
                Comment = viewModel.Comment,
                Rating = viewModel.Rating,
                Guid = viewModel.Guid,
                IsActive = viewModel.IsActive
            };
        }
    }
}
