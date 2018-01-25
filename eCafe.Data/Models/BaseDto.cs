using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Models
{
    public class BaseDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public Guid Guid { get; set; }
    }
}
