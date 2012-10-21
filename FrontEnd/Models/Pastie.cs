using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class Pastie
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public DateTime Creation { get; set; }
        public DateTime Expiration { get; set; }
    }
}