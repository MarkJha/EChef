using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("SubMenuDetail")]
    public class MenuDetail : ActiveEntity
    {
        public MenuDetail()
        {
            ImageDetails = new List<SubMenuImageDetail>();
        }

        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Receipe { get; set; }
        public int Rate { get; set; }
        public int Discount { get; set; }
        public int ServePeoples { get; set; }
        public string Quantity { get; set; }
        public string ImagePath { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsVeg { get; set; }
        public ICollection<SubMenuImageDetail> ImageDetails { get; set; }

        //public int OrderDetailId { get; set; }
        //public virtual OrderDetail OrderDetail { get; set; }
    }
}
