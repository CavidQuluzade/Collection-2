using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TaskShop
{
    static class Menu
    {
        public static void Buy(Shop shop)
        {
            RepeatType: Console.WriteLine("Enter Producttype:(use 'c' for coffee and 't' for tea");
            string type = Console.ReadLine();
            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Count: ");
            int count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Price:");
            double price = Convert.ToDouble(Console.ReadLine());

            if (shop.products.Any(p => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                Console.WriteLine("You have this product");
            }
            else
            {
                if (type == "c")
                {
                    type = "Coffee";
                    shop.products.Add(new Coffee { Name = name, Count = count, Price = price, Type = type });
                }
                else if (type == "t")
                {
                    type = "Tea";
                    shop.products.Add(new Tea { Name = name, Count = count, Price = price, Type = type});
                }
                else
                {
                    Console.WriteLine("Error: Choose 't' or 'c'");
                    goto RepeatType;
                }
            }
            
        }
        public static void Sell(Shop shop)
        {
            RepeatChoice: foreach(var product1 in shop.products) { Console.WriteLine(product1.Type + " " + product1.Name); }
            Console.WriteLine("Enter product name to sell");
            string name = Console.ReadLine();
            Console.WriteLine("Enter count");
            int count = Convert.ToInt32(Console.ReadLine());
            Product product = shop.products.FirstOrDefault(p => p.Name == name);
            if (product == null)
            {
                Console.WriteLine("You don't have this product");
                goto RepeatChoice;
            }
            if(product.Count < count)
            {
                Console.WriteLine("You don't have " + count + " " + name);
                goto RepeatChoice;
            }
            else
            {
                product.Count -= count;
                shop.TotalIncome += product.Price * count;
                Console.WriteLine($"Total income is: {shop.TotalIncome}");
            }
        }
        public static void DisplayProducts(Shop shop, Tea tea)
        {
            
            foreach (Product product in shop.products)
            {
                Console.WriteLine("Products "+ product.Name + " " + product.Type);
            }
        }
    }
}
