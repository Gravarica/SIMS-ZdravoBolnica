
using System;
using System.Collections.Generic;

namespace Model
{
   public class Patient : User
   {
      
        public int Id { get; set; }
        public String Jmbg { get; set; }
        public string Mail { get; set; }
        public string Adress { get; set; }
        public int PhoneNumber { get; set; }

        public int MedicalRecordId { get; set; }

        private List<Appointment> appointments;

      public Patient(int id, int medicalRecordId,String username, String password, string lastName) : base(username, password, lastName)
        {
            Id = id;
            MedicalRecordId = medicalRecordId;
            appointments = new List<Appointment>(); 
        }

        public Patient(String username, String firstName, string lastName, string mail, string adress, string jmbg, int phoneNumber) : base(username, firstName, lastName)
        {
            Mail = mail;
            Adress = adress;
            Jmbg = jmbg;
            PhoneNumber = phoneNumber;
            appointments = new List<Appointment>();
        }

        public Patient(int id) { 
            Id = id;
            appointments = new List<Appointment>();    
        }

        public Patient() { }
   }
}