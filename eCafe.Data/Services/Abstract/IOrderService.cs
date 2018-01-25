using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Services.Abstract
{
    public interface IOrderService
    {
       // Task<IEnumerable<OrderDetailDto>> GetOrderAsync(int pageSize = 10, int pageNumber = 1, string name = null);
        Task<IEnumerable<OrderDto>> GetOrderAsync(int pageSize = 10, int pageNumber = 1, string name = null);

        Task<OrderDto> GetOrderAsync(int id);

        Task<OrderDto> CreateOrderAsync(OrderDto model);

        Task<OrderDto> UpdateOrderAsync(MenuDto model);

        Task<OrderDto> DeleteOrderAsync(int id);
    }
}
