using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail : Entity
    {
        
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int MenuDetailId { get; set; }
        public virtual MenuDetail MenuDetail { get; set; }

        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Discount { get; set; }
        public int FinalTotal { get; set; }

    }
}
