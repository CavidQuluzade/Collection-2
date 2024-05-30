using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Course
    {
        private static int id = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Group> Groups { get; set; }
        public Course(string name) 
        {
            Groups = new List<Group>();
            Id = id++;
            Name = name;
        }
        public void AddGroup(Group group)
        {
            Groups.Add(group);
        }
        public void RemoveGroup(Group group)
        {
            Groups.Remove(group);
        }
        public void GetAllGroups() 
        {
            foreach (var group in Groups)
            {
                group.GetGroupDetails();
            }
        }
        
        

    }
}
