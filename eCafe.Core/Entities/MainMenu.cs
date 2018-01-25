using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("MainMenu")]
    public class MainMenu : ActiveEntity
    {
        public MainMenu()
        {
            Menus = new List<Menu>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Menu> Menus { get; set; }
    }
}
