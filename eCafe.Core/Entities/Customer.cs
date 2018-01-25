using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("Customer")]
    public class Customer : ActiveEntity
    {
        public Customer()
        {
            Orders = new List<Order>();
        }
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

        public ICollection<Order> Orders { get; set; }
    }
    
}
