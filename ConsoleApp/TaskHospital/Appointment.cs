using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHospital
{
    internal class Appointment
    {
        public string PatientName { get; set; }
        public DateTime Date { get; set; }
        public Appointment(string patientName, DateTime date)
        {
            PatientName = patientName;
            Date = date;
        }
    }
}
