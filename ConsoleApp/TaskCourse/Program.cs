using System;
using System.Globalization;
using System.Threading.Channels;
using System.Transactions;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Course course = new Course("Code Academy");
            bool Exit = true;
            while (Exit)
            {
            RepeatMenu: 
                Console.WriteLine("1       Add Group");
                Console.WriteLine("2       Remove Group");
                Console.WriteLine("3       Edit Group");
                Console.WriteLine("4       View Groups");
                Console.WriteLine("5       Add Student to Group");
                Console.WriteLine("6       Remove Student in Group");
                Console.WriteLine("7       View Students in Group");
                Console.WriteLine("8       Edit Student");
                Console.WriteLine("9       Search Student");
                Console.WriteLine("10      View Students in Course");
                Console.WriteLine("11      Add Grade to Student");
                Console.WriteLine("12      View Student Grade");
                Console.WriteLine("13      View All Students Grades");
                Console.WriteLine("0       Exit");
                Console.WriteLine("Enter operation: ");
                string choice = Console.ReadLine();
                bool isSucceed = int.TryParse(choice, out int result);
                if (isSucceed)
                {
                    switch (result)
                    {
                        case (int)Operations.AddGroup:
                        AddGroupDesc: Console.WriteLine("Enter group name: ");
                            string groupname = Console.ReadLine();
                            if (groupname.InvalidName())
                            {
                                if (!course.Groups.Any(x => x.Name.ToLower() == groupname.ToLower()))
                                {
                                LimitDesc: Console.WriteLine("Enter limit of students in group: ");
                                    string limit = Console.ReadLine();
                                    bool islimit = int.TryParse(limit, out int newlimit);
                                    if (islimit)
                                    {
                                        course.AddGroup(new Group(groupname, newlimit));
                                    }
                                    else
                                    {
                                        Console.WriteLine(Errormessages.FormatError);
                                        goto LimitDesc;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.SameNameError);
                                    goto AddGroupDesc;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto AddGroupDesc;
                            }
                            break;
                        case (int)Operations.ViewAllGroups:
                            course.GetAllGroups();
                            break;
                        case (int)Operations.RemoveGroup:
                        RemoveGroupDesc: course.GetAllGroups();
                            Console.WriteLine("Enter id to remove");
                            string input = Console.ReadLine();
                            isSucceed = int.TryParse(input, out int id);
                            if (isSucceed)
                            {
                                var Exist = course.Groups.FirstOrDefault(x => x.Id == id);
                                if (Exist is not null)
                                {
                                    course.RemoveGroup(Exist);
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.GroupNotFound);
                                    goto RemoveGroupDesc;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto RemoveGroupDesc;
                            }
                            break;
                        case (int)Operations.EditGroup:
                        RepeatEditGroup: course.GetAllGroups();
                            Console.WriteLine("Enter group id to edit:");
                            input = Console.ReadLine();
                            isSucceed = int.TryParse(input, out id);
                            if (isSucceed)
                            {
                                var Exist = course.Groups.FirstOrDefault(x => x.Id == id);
                                if (Exist is not null)
                                {
                                RepeatNewGroupname: Console.WriteLine("Enter new name: ");
                                    string newgroupname = Console.ReadLine();
                                    if (newgroupname.InvalidName())
                                    {
                                        if (!course.Groups.Any(x => x.Name.ToLower() == newgroupname.ToLower()))
                                        {
                                            Exist.Name = newgroupname;
                                        RepeatLimit: Console.WriteLine("Enter new limit of students in group: ");
                                            string newlimit = Console.ReadLine();
                                            bool islimit = int.TryParse(newlimit, out int limit);
                                            if (islimit)
                                            {
                                                Exist.Limit = limit;
                                            }
                                            else
                                            {
                                                Console.WriteLine(Errormessages.FormatError);
                                                goto RepeatLimit;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(Errormessages.SameNameError);
                                            goto RepeatNewGroupname;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Errormessages.FormatError);
                                        goto RepeatEditGroup;
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.GroupNotFound);
                                    goto RepeatEditGroup;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto RepeatEditGroup;
                            }
                        case (int)Operations.AddStudentToGroup:
                        RepeatGroupname: course.GetAllGroups();
                            Console.WriteLine("Enter group id where you want add student");
                            input = Console.ReadLine();
                            isSucceed = int.TryParse(input, out id);
                            if (isSucceed)
                            {
                                var Exist = course.Groups.FirstOrDefault(g => g.Id == id && g.Students.Count < g.Limit);
                                if (Exist is not null)
                                {
                                RepeatStudentname: Console.WriteLine("Enter student name: ");
                                    string studentname = Console.ReadLine();
                                    if (studentname.InvalidName())
                                    {
                                    RepeatStudentsurname: Console.WriteLine("Enter student surname: ");
                                        string studentsurname = Console.ReadLine();
                                        if (studentsurname.InvalidName())
                                        {
                                        RepeatBirthdate: Console.WriteLine("Enter student birthdate (Example dd/mm/yyyy)");
                                            var date = Console.ReadLine();
                                            DateTime birthdate;
                                            isSucceed = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate);
                                            if (isSucceed)
                                            {
                                                Exist.Students.Add(new Student(studentname, studentsurname, birthdate));
                                                Console.WriteLine("Student added");
                                            }
                                            else
                                            {
                                                Console.WriteLine(Errormessages.FormatError);
                                                goto RepeatBirthdate;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(Errormessages.FormatError);
                                            goto RepeatStudentsurname;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Errormessages.FormatError);
                                        goto RepeatStudentname;
                                    }
                                }
                                else
                                {
                                    Exist = course.Groups.FirstOrDefault(g => g.Id != id);
                                    if (Exist is not null)
                                    {
                                        Console.WriteLine(Errormessages.GroupNotFound);
                                        goto RepeatGroupname;
                                    }
                                    else
                                    {
                                        Console.WriteLine(Errormessages.LimitError);
                                        Console.WriteLine("Do you want to change group: y/n");
                                        choice = Console.ReadLine();
                                        if (choice == "y")
                                            goto RepeatGroupname;
                                        else if (choice == "n")
                                            goto RepeatMenu;
                                        else
                                            Console.WriteLine(Errormessages.FormatError);
                                        goto RepeatMenu;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto RepeatGroupname;
                            }
                            break;
                        case (int)Operations.RemoveStudentInGroup:
                            course.GetAllGroups();
                        RepeatGroupIdRemoveStudent: Console.WriteLine("Enter group id where you want remove student:");
                            input = Console.ReadLine();
                            isSucceed = int.TryParse(input, out id);
                            if (isSucceed)
                            {
                                var Exist = course.Groups.FirstOrDefault(g => g.Id == id);
                                if (Exist is not null)
                                {
                                    RepeatStudentInGroup: Exist.GetStudentsInGroup();
                                    Console.WriteLine("Enter student id to remove:");
                                    input = Console.ReadLine();
                                    isSucceed = int.TryParse(input, out id);
                                    if (isSucceed)
                                    {
                                        var ExistStudent = Exist.Students.FirstOrDefault(g => g.Id == id);
                                        if (ExistStudent is not null)
                                        {
                                            Exist.RemoveStudent(ExistStudent);
                                            Console.WriteLine("Student removed");
                                        }
                                        else
                                        {
                                            Console.WriteLine(Errormessages.StudentNotFound);
                                            goto RepeatStudentInGroup;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Errormessages.FormatError);
                                        goto RepeatStudentInGroup;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.GroupNotFound);
                                    goto RepeatGroupIdRemoveStudent;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto RepeatGroupIdRemoveStudent;
                            }
                            break;
                        case (int)Operations.ViewStudentsInGroup:
                            course.GetAllGroups();
                        RepeatGroupId: Console.WriteLine("Enter group id to view students: ");
                            input = Console.ReadLine();
                            isSucceed = int.TryParse(input, out id);
                            if (isSucceed)
                            {
                                var Exist = course.Groups.FirstOrDefault(g => g.Id == id);
                                if (Exist is not null)
                                {
                                    Exist.GetStudentsInGroup();
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.GroupNotFound);
                                    goto RepeatGroupId;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto RepeatGroupId;
                            }
                            break;
                        case (int)Operations.EditStudentInGroup:
                            RepeatEditStudent: course.GetAllGroups();
                            Console.WriteLine("Enter group id to choose student:");
                            input= Console.ReadLine();
                            isSucceed=int.TryParse(input, out id);
                            if (isSucceed)
                            {
                                var Existgroup = course.Groups.FirstOrDefault(Group => Group.Id == id);
                                if(Existgroup is not null)
                                {
                                    RepeatStudentId: Existgroup.GetStudentsInGroup();
                                    Console.WriteLine("Enter student id to edit:");
                                    input = Console.ReadLine();
                                    isSucceed = int.TryParse(input, out id);
                                    var Existstudent = Existgroup.Students.FirstOrDefault(student => student.Id == id);
                                    if (Existstudent is not null)
                                    {
                                        RepeatStudentName: Console.WriteLine("Enter new student name:");
                                        string newname = Console.ReadLine();
                                        if (newname.InvalidName())
                                        {
                                            Existstudent.Name = newname;
                                            RepeatStudentSurname: Console.WriteLine("Enter student surname");
                                            string newsurname = Console.ReadLine();
                                            if (newsurname.InvalidName())
                                            {
                                                Existstudent.Surname = newsurname;
                                                Console.WriteLine("Enter student birthdate:");
                                            RepeatStudentBirthdate: Console.WriteLine("Enter student birthdate (Example dd/mm/yyyy)");
                                                var date = Console.ReadLine();
                                                DateTime newbirthdate;
                                                isSucceed = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newbirthdate);
                                                if (isSucceed)
                                                {
                                                    Existstudent.Birthday = newbirthdate;
                                                    Console.WriteLine("Student Edited");
                                                }
                                                else
                                                {
                                                    Console.WriteLine(Errormessages.FormatError);
                                                    goto RepeatStudentBirthdate;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(Errormessages.FormatError);
                                                goto RepeatStudentSurname;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(Errormessages.FormatError);
                                            goto RepeatStudentName;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Errormessages.StudentNotFound);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.GroupNotFound);
                                    goto RepeatEditStudent;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto RepeatEditStudent;
                            }
                            break;
                        case (int)Operations.ViewStudentsInCourse:
                            foreach(var group in course.Groups)
                            {
                                foreach(var student in group.Students)
                                {
                                    student.GetStudentDetails();
                                }
                            }
                            break;
                        case (int)Operations.SearchStudent:
                            RepeatSearch: Console.WriteLine("Enter something to found students:");
                            string character = Console.ReadLine();
                            foreach (var group in course.Groups)
                            {
                                foreach (var student in group.Students)
                                {
                                    isSucceed = (student.Name.IndexOf(character, StringComparison.CurrentCultureIgnoreCase) >= 0) || (student.Surname.IndexOf(character, StringComparison.CurrentCultureIgnoreCase) >= 0);
                                }
                            }
                            if (isSucceed)
                            {
                                foreach (var group in course.Groups)
                                {
                                    foreach (var student in group.Students)
                                    {
                                        student.GetStudentDetails();
                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine(Errormessages.StudentNotFound);
                            RepeatChoice: Console.WriteLine("Do you want change search(y/n)");
                                choice = Console.ReadLine();
                                if (choice == "y")
                                {
                                    goto RepeatSearch;
                                }
                                else if (choice == "n")
                                {
                                    goto RepeatMenu;
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.FormatError);
                                    goto RepeatChoice;
                                }
                            }
                            break;
                        case (int)Operations.AddGrade:
                            RepeatGetGroups: course.GetAllGroups();
                            Console.WriteLine("Enter group id:");
                            input = Console.ReadLine();
                            isSucceed = int.TryParse(input, out id);
                            if (isSucceed)
                            {
                                var Existgroup = course.Groups.FirstOrDefault(group => group.Id == id);
                                if(Existgroup is not null)
                                {
                                    RepeatGetStudent: Existgroup.GetStudentsInGroup();
                                    Console.WriteLine("Enter student id to add grade:");
                                    input = Console.ReadLine();
                                    isSucceed= int.TryParse(input, out id);
                                    if (isSucceed)
                                    {
                                        var Existstudent = Existgroup.Students.FirstOrDefault(student => student.Id == id);
                                        if(Existstudent is not null)
                                        {
                                            RepeatSubject: Console.WriteLine("Enter subject name:");
                                            string subjectname = Console.ReadLine();
                                            if (subjectname.InvalidName())
                                            {
                                                RepeatGrade: Console.WriteLine("Enter grade [0-100]:");
                                                input = Console.ReadLine();
                                                isSucceed = int.TryParse (input, out int grade);
                                                if (isSucceed)
                                                {
                                                    if (grade.InvalidGrade())
                                                    {
                                                        Existstudent.AddGrade(new Grade(subjectname, grade));
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(Errormessages.GradeError);
                                                        goto RepeatGrade;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine(Errormessages.FormatError);
                                                    goto RepeatGrade;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(Errormessages.FormatError);
                                                goto RepeatSubject;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(Errormessages.StudentNotFound);
                                            goto RepeatGetStudent;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Errormessages.FormatError);
                                        goto RepeatGetStudent;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.GroupNotFound);
                                    goto RepeatGetGroups;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto RepeatGetGroups;
                            }
                            break;
                        case (int)Operations.ViewStudentGrade:
                        RepeatGetGroup: course.GetAllGroups();
                            Console.WriteLine("Enter group id:");
                            input = Console.ReadLine();
                            isSucceed = int.TryParse(input, out id);
                            if (isSucceed)
                            {
                                var Existgroup = course.Groups.FirstOrDefault(group => group.Id == id);
                                if (Existgroup is not null)
                                {
                                RepeatGetStudents: Existgroup.GetStudentsInGroup();
                                    Console.WriteLine("Enter student id to add grade:");
                                    input = Console.ReadLine();
                                    isSucceed = int.TryParse(input, out id);
                                    if (isSucceed)
                                    {
                                        var Existstudent = Existgroup.Students.FirstOrDefault(student => student.Id == id);
                                        if (Existstudent is not null)
                                        {
                                            Existstudent.GetStudentGrades(Existstudent.Name, Existstudent.Surname);
                                        }
                                        else
                                        {
                                            Console.WriteLine(Errormessages.StudentNotFound);
                                            goto RepeatGetStudents;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Errormessages.FormatError);
                                        goto RepeatGetStudents;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Errormessages.GroupNotFound);
                                    goto RepeatGetGroup;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Errormessages.FormatError);
                                goto RepeatGetGroup;
                            }
                            break;
                        case (int)Operations.ViewGrades:
                            foreach (var group in course.Groups)
                            {
                                foreach (var student in group.Students)
                                {
                                    student.GetStudentGrades(student.Name, student.Surname);
                                }
                            }
                            break;
                        case (int)Operations.Exit:
                            Exit = false;
                            break;
                        default:
                            Console.WriteLine(Errormessages.OperationError);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(Errormessages.FormatError);
                    goto RepeatMenu;
                }
            }
        }
    }
}
