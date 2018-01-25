using eCafe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Models
{
    public class MenuDetailDto :BaseDto, IEntityBase
    {
        public int MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Receipe { get; set; }
        public int Rate { get; set; }
        public int Discount { get; set; }
        public int ServePeoples { get; set; }
        public string Quantity { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsVeg { get; set; }
        public IEnumerable<MenuImageDetailDto> ImageDetails { get; set; }
        public double MatchPercent { get; internal set; }
        public string ImagePath { get; set; }
    }
}
