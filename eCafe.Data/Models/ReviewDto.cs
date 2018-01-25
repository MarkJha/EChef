using eCafe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Models
{
    public class ReviewDto : BaseDto, IEntityBase
    {
        public int CustId { get; set; }
        public int SubMenuId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public MenuDetailDto MenuDetail { get; set; }
    }
}
