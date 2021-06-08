using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public class OrderProduct
    {
        public string ID { get; set; }
        public string BookId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
