using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Errormessages
    {
        public static string FormatError = "Format: Error";
        public static string SameNameError = "This name already exist";
        public static string GroupNotFound = "You don't have this group";
        public static string OperationError = "Enter corect operation";
        public static string LimitError = "Group is full";
        public static string StudentNotFound = "You don;t have this student in this group";
        public static string GradeError = "Enter correct grade [0 - 100]";
    }
}
