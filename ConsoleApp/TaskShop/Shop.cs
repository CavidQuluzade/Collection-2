using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TaskShop
{
    internal class Shop
    {
        public double TotalIncome = 0;
        public List<Product> products;
        public Shop()
        {
            products = new List<Product>();
        }
    }
}
