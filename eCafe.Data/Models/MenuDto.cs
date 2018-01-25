using eCafe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Models
{
    public class MenuDto : BaseDto, IEntityBase
    {

        public int MainMenuId { get; set; }
        public string MenuName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

    }
}
