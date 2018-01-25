using eCafe.Core.Entities;
using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCafe.Infrastructure.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToViewModel(this Customer entity)
        {
            return entity == null ? null : new CustomerDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address1 = entity.Address1,
                Address2 = entity.Address2,
                City = entity.City,
                ContactNo = entity.ContactNo,
                Email = entity.Email,
                NearBy = entity.NearBy,
                PinCode = entity.PinCode,
                Point = entity.Point,
                ProfilePic = entity.ProfilePic,
                Sex = entity.Sex,
                StreetName = entity.StreetName,
                Guid = entity.Guid,
                IsActive = entity.IsActive,
                Orders = (from ord in entity.Orders
                          select new OrderDto
                          {
                              Id = ord.Id,
                              NetAmount = ord.NetAmount,
                              CustId = ord.CustomerId,
                              Guid = ord.Guid,
                              OrderStatus = ord.OrderStatus,
                              TotalDiscount = ord.TotalDiscount,
                              TotalPrice = ord.TotalPrice,
                              TotalQuantity = ord.TotalQuantity
                          }).ToList()

            };
        }

        /// <summary>
        /// Convert view model to db entity
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static Customer ToEntity(this CustomerDto viewModel)
        {
            return viewModel == null ? null : new Customer
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Address1 = viewModel.Address1,
                Address2 = viewModel.Address2,
                City = viewModel.City,
                ContactNo = viewModel.ContactNo,
                Email = viewModel.Email,
                NearBy = viewModel.NearBy,
                PinCode = viewModel.PinCode,
                Point = viewModel.Point,
                ProfilePic = viewModel.ProfilePic,
                Sex = viewModel.Sex,
                StreetName = viewModel.StreetName,
                Guid = viewModel.Guid,
                IsActive = viewModel.IsActive
            };
        }
    }
}
