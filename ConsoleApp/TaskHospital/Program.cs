using System.Globalization;
using System.Numerics;
using System.Transactions;

namespace TaskHospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();
            bool Exit = true;
            while (Exit)
            {
                RepeatMenu: 
                Console.WriteLine("1     Add Doctor");
                Console.WriteLine("2     View all Doctors");
                Console.WriteLine("3     Shedule Appointment");
                Console.WriteLine("4     View all Appointments of Doctor");
                Console.WriteLine("Enter operation:");
                string choice = Console.ReadLine();
                bool isSucceed = int.TryParse(choice, out int result);
                if (isSucceed)
                {
                    switch (result)
                    {
                        case (int)Choice.AddDoctor:
                            Console.WriteLine("Enter name of doctor:");
                            string name = Console.ReadLine();
                            if (name.InvalidName())
                            {
                                hospital.AddDoctor(new Doctor(name));
                            }
                            else
                            {
                                Console.WriteLine(ErrorMessages.FormatError);
                            }
                            break;
                        case (int)Choice.ViewAllDoctors:
                            foreach(var doctor in hospital.Doctors)
                            {
                                Console.WriteLine("Doctor: " + doctor.Name);
                            }
                            break;
                        case (int)Choice.SheduleAppointment:
                            RepeatShedule: foreach (var doctor in hospital.Doctors)
                            {
                                Console.WriteLine("Doctor: " + doctor.Name);
                            }
                            Console.WriteLine("Enter doctor name:");
                            name = Console.ReadLine();
                            var Existdoctor = hospital.Doctors.FirstOrDefault(x => x.Name == name);
                            if(Existdoctor != null)
                            {
                                Console.WriteLine("Choose time according this appointments");
                                Console.WriteLine("Time between appointments must be at least 1 hour");
                                foreach (var appointment in Existdoctor.Appointments)
                                {
                                    Console.WriteLine(appointment.PatientName + " - " + appointment.Date);
                                }
                            RepeatPatientName: Console.WriteLine("Enter patient name:");
                                string patientname = Console.ReadLine();
                                if (patientname.InvalidName())
                                {
                                    RepeatDate: Console.WriteLine("Enter time Example(Day/Month/Year/Hour/Minute");
                                    var date = Console.ReadLine();
                                    DateTime arrivedate;
                                    isSucceed = DateTime.TryParseExact(date,"dd/MM/yyyy/HH/mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out arrivedate);
                                    if (isSucceed)
                                    {

                                        var ExistTime = Existdoctor.Appointments.FirstOrDefault(date => Math.Abs((date.Date-arrivedate).TotalHours)<1);
                                        if(ExistTime is null)
                                        {
                                            Existdoctor.AddAppointment(new Appointment(patientname, arrivedate));
                                        }
                                        else
                                        {
                                            Console.WriteLine(ErrorMessages.AppointmentError);
                                            goto RepeatDate;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(ErrorMessages.FormatError);
                                        goto RepeatDate;
                                    }
                                }
                                else 
                                {
                                    Console.WriteLine(ErrorMessages.FormatError);
                                    goto RepeatPatientName;
                                }
                            }
                            else
                            {
                                Console.WriteLine(ErrorMessages.DoctorNotFound);
                                goto RepeatShedule;
                            }
                            break;
                        case (int)Choice.ViewAllAppointmentsOfDoctors:
                            RepeatViewAppointment: foreach (var doctor in hospital.Doctors)
                            {
                                Console.WriteLine("Doctor " + doctor.Name);
                            }
                            Console.WriteLine("Enter doctor name:");
                            name = Console.ReadLine();
                            if (name.InvalidName())
                            {
                                Existdoctor = hospital.Doctors.FirstOrDefault(x => x.Name == name);
                                if (Existdoctor is not null)
                                {
                                    foreach (var appointment in Existdoctor.Appointments)
                                    {
                                        Console.WriteLine(appointment.PatientName + " - " + appointment.Date);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(ErrorMessages.DoctorNotFound);
                                    goto RepeatViewAppointment;
                                }
                            }
                            else
                            {
                                Console.WriteLine(ErrorMessages.FormatError);
                                goto RepeatViewAppointment;
                            }
                            break;
                        case (int)Choice.Exit:
                            Exit = false;
                            break;
                        default: Console.WriteLine(ErrorMessages.FormatError);
                            goto RepeatMenu;
                    }
                }
                else
                {
                    Console.WriteLine(ErrorMessages.FormatError);
                    goto RepeatMenu;
                }
            }
        }
    }
}
