using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHospital
{
    public static class StringExtention
    {
        public static bool InvalidName(this string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && !(name.Length < 2)) return true;
            else return false;
        }
    }
}
