using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("Review")]
    public class Review : ActiveEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int SubMenuDetailId { get; set; }
        public virtual MenuDetail SubMenuDetail { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }

    }
}
