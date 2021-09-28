using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNet5ApiAdoTest.Models
{
    public class NewsInfo
    {
        public int NewsId { get; set; }

        public string NewsTitle { get; set; }

        public string NewsContent { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? NewsTypeId { get; set; }
    }
}
