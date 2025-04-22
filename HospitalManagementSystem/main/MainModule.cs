using HospitalManagementSystem.dao;
using HospitalManagementSystem.entity;
using HospitalManagementSystem.myexception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            IHospitalService service = new HospitalServiceImpl();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Hospital Management System ---");
                Console.WriteLine("1. Get Appointment by ID");
                Console.WriteLine("2. Get Appointments for Patient");
                Console.WriteLine("3. Get Appointments for Doctor");
                Console.WriteLine("4. Schedule Appointment");
                Console.WriteLine("5. Update Appointment");
                Console.WriteLine("6. Cancel Appointment");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Appointment ID: ");
                            int aid = int.Parse(Console.ReadLine());
                            var appointment = service.GetAppointmentById(aid);
                            Console.WriteLine(appointment);
                            break;

                        case "2":
                            Console.Write("Enter Patient ID: ");
                            int pid = int.Parse(Console.ReadLine());
                            var patientAppointments = service.GetAppointmentsForPatient(pid);
                            patientAppointments.ForEach(Console.WriteLine);
                            break;

                        case "3":
                            Console.Write("Enter Doctor ID: ");
                            int did = int.Parse(Console.ReadLine());
                            var doctorAppointments = service.GetAppointmentsForDoctor(did);
                            doctorAppointments.ForEach(Console.WriteLine);
                            break;

                        case "4":
                            Console.WriteLine("Enter Appointment Details:");
                            Console.Write("Appointment ID: ");
                            int newId = int.Parse(Console.ReadLine());
                            Console.Write("Patient ID: ");
                            int newPid = int.Parse(Console.ReadLine());
                            Console.Write("Doctor ID: ");
                            int newDid = int.Parse(Console.ReadLine());
                            Console.Write("Date (yyyy-mm-dd): ");
                            DateTime date = DateTime.Parse(Console.ReadLine());
                            Console.Write("Description: ");
                            string desc = Console.ReadLine();
                            var newAppt = new Appointment(newId, newPid, newDid, date, desc);
                            bool added = service.ScheduleAppointment(newAppt);
                            Console.WriteLine(added ? "Appointment scheduled." : "Failed to schedule.");
                            break;

                        case "5":
                            Console.WriteLine("Enter Updated Appointment Details:");
                            Console.Write("Appointment ID: ");
                            int updId = int.Parse(Console.ReadLine());
                            Console.Write("Patient ID: ");
                            int updPid = int.Parse(Console.ReadLine());
                            Console.Write("Doctor ID: ");
                            int updDid = int.Parse(Console.ReadLine());
                            Console.Write("Date (yyyy-mm-dd): ");
                            DateTime updDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Description: ");
                            string updDesc = Console.ReadLine();
                            var updAppt = new Appointment(updId, updPid, updDid, updDate, updDesc);
                            bool updated = service.UpdateAppointment(updAppt);
                            Console.WriteLine(updated ? "Appointment updated." : "Failed to update.");
                            break;

                        case "6":
                            Console.Write("Enter Appointment ID to cancel: ");
                            int cancelId = int.Parse(Console.ReadLine());
                            bool cancelled = service.CancelAppointment(cancelId);
                            Console.WriteLine(cancelled ? "Appointment cancelled." : "Failed to cancel.");
                            break;

                        case "0":
                            running = false;
                            Console.WriteLine("Exiting...");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (PatientNumberNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
            }
        }
    }
}
