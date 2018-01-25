using eCafe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Models
{
    public class OrderDetailDto : IEntityBase
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderDto Order { get; set; }

        public int SubMenuId { get; set; }
        public MenuDetailDto MenuDetail { get; set; }

        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Discount { get; set; }
        public int FinalTotal { get; set; }

    }
}
