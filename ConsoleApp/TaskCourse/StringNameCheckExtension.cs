using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class StringNameCheckExtension
    {
        public static bool InvalidName(this string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && !(name.Length < 2)) return true;
            else return false;
        }
        public static bool InvalidGrade(this int grade) 
        {
            if (grade < 0 && grade > 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
