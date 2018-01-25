using eCafe.Core.Entities;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Repository
{
    public class OrderRepository : EntityBaseRepository<Order>, IOrderRepository
    {
        private ECafeContext _context;

        public OrderRepository(ECafeContext context)
            : base(context)
        {
            _context = context;
        }


        public async Task<int> DeleteOrderAsync(int OrderId)
        {
            var OrderDetailObject = _context.OrderDetails.Where(i => i.OrderId.Equals(OrderId));
            if (OrderDetailObject != null)
            {
                _context.OrderDetails.RemoveRange(OrderDetailObject);

                var orderDeletedObject = _context
                                        .Orders
                                        .FirstOrDefault(m => m.Id.Equals(OrderId));

                if (orderDeletedObject != null)
                {
                    _context.Orders.Remove(orderDeletedObject);
                    return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }
    }
}
