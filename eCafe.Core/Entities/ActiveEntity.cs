using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Core.Entities
{
    public class ActiveEntity : Entity, IActive
    {
        public bool IsActive { get; set; }
        public Guid Guid { get; set; }
        //public Guid Guid
        //{
        //    get { return Guid; }
        //    set { value = new Guid(); }
        //}
    }
}
