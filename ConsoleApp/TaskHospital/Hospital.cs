using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHospital
{
    internal class Hospital
    {
        public List<Doctor> Doctors { get; set; }
        public Hospital() 
        {
            Doctors = new List<Doctor>();
        }
        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
        }
    }
}
