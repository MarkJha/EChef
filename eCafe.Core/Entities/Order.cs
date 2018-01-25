using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("Order")]
    public class Order : Entity
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int TotalQuantity { get; set; }
        public int TotalPrice { get; set; }
        public int TotalDiscount { get; set; }
        public int NetAmount { get; set; }
        //public int NetAmount
        //{
        //    get { return NetAmount; }
        //    set { value = TotalQuantity * TotalPrice * TotalDiscount / 100; }
        //}
        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Guid Guid { get; set; }
    }
}
