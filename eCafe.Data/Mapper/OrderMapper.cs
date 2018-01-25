using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCafe.Infrastructure.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto ToViewModel(this Order entity)
        {
            return entity == null ? null : new OrderDto
            {
                Id = entity.Id,
                CustId = entity.CustomerId,
                NetAmount = entity.NetAmount,
                OrderStatus = entity.OrderStatus,
                TotalDiscount = entity.TotalDiscount,
                TotalPrice = entity.TotalPrice,
                TotalQuantity = entity.TotalQuantity,
                Guid = entity.Guid,
                //OrderDetails = (from order in entity.OrderDetails
                //                select new OrderDetailDto
                //                {
                //                    Id=order.Id,
                //                    OrderId=order.OrderId,
                //                    SubMenuId=order.SubMenuId,
                //                    MenuDetails = (from menuDetail in order.MenuDetails
                //                                   select new MenuDetailDto
                //                                   {
                //                                       MenuName = menuDetail.Name,
                //                                       MenuId = menuDetail.Id,
                //                                       Rate = menuDetail.Rate
                //                                   }).ToList()

                //                }).ToList()
            };
        }


        //public static Order ToEntity(this OrderDto viewModel)
        //{
        //    return viewModel == null ? null : new Order
        //    {
        //        Id = viewModel.Id,
        //        CustomerId = viewModel.CustId,
        //        NetAmount = viewModel.NetAmount,
        //        OrderStatus = viewModel.OrderStatus,
        //        TotalDiscount = viewModel.TotalDiscount,
        //        TotalPrice = viewModel.TotalPrice,
        //        TotalQuantity = viewModel.TotalQuantity,
        //        Guid = viewModel.Guid
        //    };
        //}
    }
}
