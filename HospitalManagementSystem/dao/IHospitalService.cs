using HospitalManagementSystem.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.dao
{
    public interface IHospitalService
    {
        Appointment GetAppointmentById(int appointmentId);
        List<Appointment> GetAppointmentsForPatient(int patientId);
        List<Appointment> GetAppointmentsForDoctor(int doctorId);
        bool ScheduleAppointment(Appointment appt);
        bool UpdateAppointment(Appointment appt);
        bool CancelAppointment(int appointmentId);
    }
}
