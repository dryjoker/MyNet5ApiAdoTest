using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNet5ApiAdoTest.Models
{
    public class NewsType
    {
        public int NewsTypeId { get; set; }

        public string NewsTypeName { get; set; }

        public bool? isEnabled { get; set; }

    }
}
