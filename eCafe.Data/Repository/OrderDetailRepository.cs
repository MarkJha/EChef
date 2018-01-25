using eCafe.Core.Entities;
using eCafe.Infrastructure.Context;
using eCafe.Infrastructure.Repository.Interface;

namespace eCafe.Infrastructure.Repository
{
    public class OrderDetailRepository : EntityBaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ECafeContext context)
            : base(context)
        {
        }

        

    }
}
