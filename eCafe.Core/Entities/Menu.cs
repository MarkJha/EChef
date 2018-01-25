using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("Menu")]
    public class Menu : ActiveEntity
    {
        public Menu()
        {
            SubMenuDetails = new List<MenuDetail>();
        }
        public int MainMenuId { get; set; }
        public virtual MainMenu MainMenu { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public ICollection<MenuDetail> SubMenuDetails { get; set; }
    }
}
