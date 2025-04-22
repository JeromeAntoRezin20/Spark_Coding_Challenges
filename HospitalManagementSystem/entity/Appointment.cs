using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.entity
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }

        public Appointment() { }

        
        public Appointment(int appointmentID, int patientID, int doctorID, DateTime appointmentDate, string description)
        {
            AppointmentID = appointmentID;
            PatientID = patientID;
            DoctorID = doctorID;
            AppointmentDate = appointmentDate;
            Description = description;
        }

        public override string ToString()
        {
            return $"AppointmentID: {AppointmentID}, PatientID: {PatientID}, DoctorID: {DoctorID}, Date: {AppointmentDate.ToShortDateString()}, Description: {Description}";
        }

    }
}
