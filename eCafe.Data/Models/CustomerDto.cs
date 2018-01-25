using eCafe.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Models
{
    public class CustomerDto : BaseDto, IEntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Sex { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public string NearBy { get; set; }
        public string StreetName { get; set; }
        public string ProfilePic { get; set; }
        public int Point { get; set; }

        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
