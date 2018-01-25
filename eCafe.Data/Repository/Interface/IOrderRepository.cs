using eCafe.Core.Entities;
using eCafe.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Repository.Interface
{
    public interface IOrderRepository : IEntityBaseRepository<Order>
    {
        Task<int> DeleteOrderAsync(int OrderId);
    }
}
