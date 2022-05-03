
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
           set
           {
               id = value;
            
           }
       }

       public BloodType BloodType
       {
           get { return bloodType; }
           set { this.bloodType = value; }
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

      public Patient(
          int id, 
          int medicalRecordId,
          bool guest,
          String username,
          String firstName,
          String lastName,
          int jmbg,
          int phoneNumber,
          string email,
          string adress,
          DateTime dateofBirth,
          Gender gender) : base(username,
                                firstName,
                                lastName,
                                jmbg,
                                phoneNumber,
                                email,
                                adress,
                                dateofBirth,
                                gender)
      {
          Id = id;
          MedicalRecordId = medicalRecordId;
          Guest = false;
          appointments = new List<Appointment>();
          
      }

        //kad se menja , ostaje ID
        public Patient(
            int medicalRecordId,
            bool guest,
            String username,
            String firstName,
            String lastName,
            int jmbg,
            int phoneNumber,
            string email,
            string adress,
            DateTime dateofBirth,
            Gender gender) : base(  username,
                                    firstName,
                                    lastName,
                                    jmbg,
                                    phoneNumber,
                                    email,
                                    adress,
                                    dateofBirth,
                                    gender)
        {   
            MedicalRecordId = medicalRecordId;
            Guest = false;
            appointments = new List<Appointment>();
        }

        public Patient(int id, int medicalRecordId,
            String firstName,
            String lastName,
            int jmbg) : base(firstName, lastName, jmbg)
        {
            Id = id;
            this.bloodType = BloodType.a;
            this.guest = true;
            appointments = new List<Appointment>();
        }
        public Patient(int id) { 
            Id = id;
            appointments = new List<Appointment>();    
        }

        public Patient() { }
   }
}