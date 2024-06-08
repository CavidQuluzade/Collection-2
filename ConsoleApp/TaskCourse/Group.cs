using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Group
    {
        public string Name { get; set; }
        private static int id { get; set; } = 1;
        public int Id { get; set; }
        public int Limit { get; set; }
        public CustomList<Student> Students { get; set;}
        public Group(string name, int limit)
        {
            Id = id++;
            Students = new CustomList<Student>();
            Name = name;
            Limit = limit;
        }
        public void GetGroupDetails()
        {
            Console.WriteLine($"Id: {Id} | Groupname: {Name} | Limit: {Limit}");
        }
        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }
        public void GetStudentsInGroup()
        {
            foreach(var student in Students)
            {
                student.GetStudentDetails();
            }
        }
    }
}
