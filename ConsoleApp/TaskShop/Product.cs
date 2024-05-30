using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskShop
{
    abstract class Product
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
    }
}
