using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHospital
{
    internal class Doctor
    {
        public string Name { get; set; }
        public List<Appointment> Appointments { get; set; }
        public Doctor(string name)
        {
            Name = name;
            Appointments = new List<Appointment>();
        }
        public void AddAppointment(Appointment appointment)
        {
            Appointments.Add(appointment);
        }
    }
}
