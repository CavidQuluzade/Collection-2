using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Student
    {
        private static int id = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public List<Grade> Grades { get; set; }
        public Student(string name, string surname, DateTime birthday)
                                                   
        {
            Id = id++;
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Grades = new List<Grade>();
        }
        public void AddGrade(Grade grade)
        {
            Grades.Add(grade);
        }
        public void GetStudentGrades(string name, string surname)
        {
            foreach(var grade in Grades)
            {
                grade.GradeDetails(name, surname);
            } 
        }
        public void GetStudentDetails()
        {
            Console.WriteLine($"Id: {Id} | Surname: {Surname} | Name: {Name} | Birthdate: {Birthday}");
        }
    }
}
