using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCafe.Core.Entities
{
    [Table("LogError")]
    public class Error : IEntityBase
    {
        public int Id { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string ActionName { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public string Url { get; set; }
        public int StatusCode { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
