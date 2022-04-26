
using System;
using System.Collections.Generic;

namespace Model
{
   public class Patient : User
   {
       private int id;
       private BloodType bloodType;
       private bool guest;
       public int Id
       {
           get { return id; }
           set { id = value; }
       }

       public BloodType BloodType
       {
           get { return bloodType; }
           set { bloodType = value; }
       }

       public bool Guest 
       {
           get { return guest; }
           set { guest = value; }
       }

        public int MedicalRecordId { get; set; }

        private List<Appointment> appointments;

      public Patient(int id, int medicalRecordId,String username, String password, string lastName) : base(username, password, lastName)
        {
            Id = id;
            MedicalRecordId = medicalRecordId;
            appointments = new List<Appointment>(); 
        }

        public Patient(String username, String firstName, string lastName, string email, string adress, int jmbg, int phoneNumber) : base(username, firstName, lastName)
        {
            Email = email;
            Adress = adress;
            Jmbg = jmbg;
            PhoneNumber = phoneNumber;
            appointments = new List<Appointment>();
        }
        public Patient(
            BloodType bloodtype, 
            bool guest,
            String username,
            String firstName,
            String lastName,
            UserType userType,
            int jmbg,
            int phoneNumber,
            string email,
            string adress,
            DateTime dateofBirth,
            Gender gender) : base(  username,
                                    firstName,
                                    lastName,
                                    userType,
                                    jmbg,
                                    phoneNumber,
                                    email,
                                    adress,
                                    dateofBirth,
                                    gender)
        {
            BloodType = bloodtype;
            Guest = guest;
            appointments = new List<Appointment>();
        }

        public Patient(
            bool guest,
            String firstName,
            String lastName,
            int jmbg) : base(firstName, lastName)
        {
            
            this.guest = true;
            this.jmbg = jmbg;

            appointments = new List<Appointment>();
        }
        public Patient(int id) { 
            Id = id;
            appointments = new List<Appointment>();    
        }

        public Patient() { }
   }
}