using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.entity
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public Patient() { }

        public Patient(int patientId, string firstName, string lastName, DateTime dob, string gender, string contactNumber, string address)
        {
            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Gender = gender;
            ContactNumber = contactNumber;
            Address = address;
        }

        public override string ToString() => $"ID: {PatientId}, Name: {FirstName} {LastName}, DOB: {DateOfBirth.ToShortDateString()}, Gender: {Gender}, Phone: {ContactNumber}, Address: {Address}";
    }
}

