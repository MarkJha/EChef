using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("SubMenuImageDetail")]
    public class SubMenuImageDetail : Entity
    {
        public int MenuDetailId { get; set; }
        public virtual MenuDetail MenuDetail { get; set; }

        public string ImagePath { get; set; }
        public string ImageDesc { get; set; }
    }
}
