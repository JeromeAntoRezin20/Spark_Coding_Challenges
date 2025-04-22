using HospitalManagementSystem.entity;
using HospitalManagementSystem.myexception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using HospitalManagementSystem.util;
using HospitalManagementSystem.dao;




namespace HospitalManagementSystem.dao
{
    public class HospitalServiceImpl : IHospitalService
    {
        public Appointment GetAppointmentById(int appointmentId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Appointments WHERE AppointmentID = @AppointmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Appointment(
                                (int)reader["AppointmentID"],
                                (int)reader["PatientID"],
                                (int)reader["DoctorID"],
                                (DateTime)reader["AppointmentDate"],
                                reader["Description"].ToString()
                            );
                        }
                        else
                        {
                            throw new PatientNumberNotFoundException("Appointment ID not found.");
                        }
                    }
                }
            }
        }

        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            var list = new List<Appointment>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Appointments WHERE PatientID = @PatientID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientID", patientId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Appointment(
                                (int)reader["AppointmentID"],
                                (int)reader["PatientID"],
                                (int)reader["DoctorID"],
                                (DateTime)reader["AppointmentDate"],
                                reader["Description"].ToString()
                            ));
                        }
                    }
                }
            }
            return list;
        }

        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            var list = new List<Appointment>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Appointments WHERE DoctorID = @DoctorID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DoctorID", doctorId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Appointment(
                                (int)reader["AppointmentID"],
                                (int)reader["PatientID"],
                                (int)reader["DoctorID"],
                                (DateTime)reader["AppointmentDate"],
                                reader["Description"].ToString()
                            ));
                        }
                    }
                }
            }
            return list;
        }

        public bool ScheduleAppointment(Appointment appt)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Appointments (AppointmentID, PatientID, DoctorID, AppointmentDate, Description) " +
                               "VALUES (@AppointmentID, @PatientID, @DoctorID, @Date, @Description)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appt.AppointmentID);
                    cmd.Parameters.AddWithValue("@PatientID", appt.PatientID);
                    cmd.Parameters.AddWithValue("@DoctorID", appt.DoctorID);
                    cmd.Parameters.AddWithValue("@Date", appt.AppointmentDate);
                    cmd.Parameters.AddWithValue("@Description", appt.Description);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateAppointment(Appointment appt)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "UPDATE Appointments SET PatientID = @PatientID, DoctorID = @DoctorID, " +
                               "AppointmentDate = @Date, Description = @Description WHERE AppointmentID = @AppointmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appt.AppointmentID);
                    cmd.Parameters.AddWithValue("@PatientID", appt.PatientID);
                    cmd.Parameters.AddWithValue("@DoctorID", appt.DoctorID);
                    cmd.Parameters.AddWithValue("@Date", appt.AppointmentDate);
                    cmd.Parameters.AddWithValue("@Description", appt.Description);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool CancelAppointment(int appointmentId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Appointments WHERE AppointmentID = @AppointmentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}

