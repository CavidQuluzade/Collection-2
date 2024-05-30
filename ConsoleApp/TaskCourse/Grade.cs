using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Grade
    {
        public int Grades {  get; set; }
        public string Subject { get; set; }
        public Grade(string subject, int grades)
        {
            Grades = grades;
            Subject = subject;
        }
        public void GradeDetails(string name, string surname)
        {
            Console.WriteLine($"{name} {surname} - {Subject}: {Grades}");
        }
    }
}
