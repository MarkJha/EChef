using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;
using System.Linq;

namespace eCafe.Infrastructure.Mapper
{
    public static class OrderDetailMapper
    {
        public static OrderDto ToViewModel(this OrderDetail entity)
        {
            return entity == null ? null : new OrderDto
            {
                Id = entity.Order.Id,
                CustId = entity.Order.CustomerId,
                NetAmount = entity.Order.NetAmount,
                OrderStatus = entity.Order.OrderStatus,
                TotalDiscount = entity.Order.TotalDiscount,
                TotalPrice = entity.Order.TotalPrice,
                TotalQuantity = entity.Order.TotalQuantity,
                Guid = entity.Order.Guid,
                OrderDetails = (from orderDet in entity.Order.OrderDetails
                                select new OrderDetailDto
                                {
                                    Id = orderDet.Id,
                                    OrderId = orderDet.OrderId,
                                    SubMenuId = orderDet.MenuDetail.Id,
                                    MenuDetail = orderDet.MenuDetail?.ToDto(),
                                    Quantity = orderDet.Quantity,
                                    Discount = orderDet.Discount,
                                    Rate = orderDet.Rate,
                                    FinalTotal = orderDet.FinalTotal

                                }).ToList()
            };
        }       

        private static MenuDetailDto ToDto(this MenuDetail menuDetail)
        {
            return new MenuDetailDto
            {
                Id = menuDetail.Id,
                MenuId = menuDetail.MenuId,
                Name = menuDetail.Name,
                Description = menuDetail.Description,
                Discount = menuDetail.Discount,
                Ingredients = menuDetail.Ingredients,
                IsSpecial = menuDetail.IsSpecial,
                IsVeg = menuDetail.IsVeg,
                MenuName = menuDetail.Menu?.Name,
                Quantity = menuDetail.Quantity,
                Receipe = menuDetail.Receipe,
                IsActive = menuDetail.IsActive,
                ServePeoples = menuDetail.ServePeoples,
                Guid = menuDetail.Guid,
                Rate = menuDetail.Rate
            };
        }


        public static OrderDetail ToEntity(this OrderDetailDto viewModel)
        {
            return viewModel == null ? null : new OrderDetail
            {
                Id = viewModel.Id,
                OrderId = viewModel.OrderId,
                MenuDetailId = viewModel.SubMenuId,
                Quantity = viewModel.Quantity,
                Rate = viewModel.Rate,
                Discount = viewModel.Discount,
                FinalTotal = viewModel.Quantity * viewModel.Rate - viewModel.Discount
            };
        }

        public static Order ToEntity(this OrderDto viewModel)
        {
            return viewModel == null ? null : new Order
            {
                Id = viewModel.Id,
                CustomerId = viewModel.CustId,
                OrderStatus = viewModel.OrderStatus,
                TotalDiscount = viewModel.TotalDiscount,
                TotalQuantity = viewModel.OrderDetails.Sum(x => x.Quantity),
                TotalPrice = viewModel.OrderDetails.Sum(x => x.Rate * x.Quantity),
                NetAmount = viewModel.OrderDetails.Sum(x => x.Rate * x.Quantity) - viewModel.TotalDiscount,
                Guid = viewModel.Guid
            };
        }
    }
}
