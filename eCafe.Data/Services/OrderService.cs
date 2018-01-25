using eCafe.Infrastructure.Services.Abstract;
using System;
using System.Collections.Generic;
using eCafe.Infrastructure.Models;
using System.Threading.Tasks;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Mapper;
using System.Linq;
using eCafe.Core.Entities;

namespace eCafe.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderDetailRepository orderDetailRepository,
            IOrderRepository orderRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto model)
        {
            var entity = await _orderRepository
                          .InsertAsync(model.ToEntity());

            foreach (var item in model.OrderDetails)
            {
                item.OrderId = entity.Id;
                await _orderDetailRepository.InsertAsync(item.ToEntity());
            }

            return entity.ToViewModel();
        }

        public async Task<OrderDto> DeleteOrderAsync(int id)
        {
            var entity = await _orderRepository.GetAsync(id);

            var isDeleted = await _orderRepository
                                 .DeleteOrderAsync(id);

            entity.Id = isDeleted != 0 ? entity.Id : 0;

            return entity.ToViewModel();
        }

        public async Task<IEnumerable<OrderDto>> GetOrderAsync(int pageSize = 10, int pageNumber = 1, string name = null)
        {
            var model = await _orderDetailRepository
                          .AllIncludingAsync(m => m.MenuDetail, o => o.Order);

            return model.GroupBy(o => new { Order = o.Order })
                        .Select(g => new OrderDetail
                        {
                            Order = g.Key.Order
                        })
                        .OrderByDescending(a => a.OrderId)
                        .Skip(pageNumber - 1 * pageSize)
                        .Take(pageSize)
                        .Select(item => item.ToViewModel())
                        .ToList();
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var model = _orderDetailRepository
                        .GetSingle(x => x.Id.Equals(id), m => m.MenuDetail, o => o.Order);

            return model.ToViewModel();
        }

        public Task<OrderDto> UpdateOrderAsync(MenuDto model)
        {
            throw new NotImplementedException();
        }
    }
}
