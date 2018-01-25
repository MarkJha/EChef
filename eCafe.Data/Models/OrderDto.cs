using eCafe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Models
{
    public class OrderDto : IEntityBase
    {
        public int Id { get; set; }
        public int CustId { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalPrice { get; set; }
        public int TotalDiscount { get; set; }
        public int NetAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IEnumerable<OrderDetailDto> OrderDetails { get; set; }
        public Guid Guid { get; set; }
    }
}
