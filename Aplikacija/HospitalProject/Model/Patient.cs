
using System;
using System.Collections.Generic;

namespace Model
{
   public class Patient : User 
   {
       private int _id;
       private BloodType _bloodType;
       private bool _guest;
       public int MedicalRecordId { get; set; }
       private List<Appointment> _appointments;
       
       public int Id
       {
           get{ return _id; }
           set{ _id = value;}
       }
  
        public BloodType BloodType
       {
           get { return _bloodType; }
           set { this._bloodType = value; OnPropertyChanged(nameof(BloodType)); }
       }

       public bool Guest 
       {
           get { return _guest; }
           set { _guest = value; OnPropertyChanged(nameof(Guest)); }
       }

        

      public Patient(int id, int medicalRecordId,String username, String password, string lastName) : base(username, password, lastName)
        {
            Id = id;
            MedicalRecordId = medicalRecordId;
            _appointments = new List<Appointment>(); 
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
          Guest = guest;
          Gender = gender;
          _appointments = new List<Appointment>();
          
      }

        //update constructor
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
            _appointments = new List<Appointment>();
        }

        public Patient(int id, int medicalRecordId,
            String firstName,
            String lastName,
            int jmbg) : base(firstName, lastName, jmbg)
        {
            Id = id;
            this._bloodType = BloodType.a;
            this._guest = true;
            _appointments = new List<Appointment>();
        }
        
        public Patient(int id) { 
            Id = id;
            _appointments = new List<Appointment>();    
        }

        public Patient(
          int id,
          int medicalRecordId,
          bool guest,
          String username,
          String password,
          String firstName,
          String lastName,
          int jmbg,
          int phoneNumber,
          string email,
          string adress,
          DateTime dateofBirth,
          Gender gender) : base(username,
                                password,
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
            Guest = guest;
            _appointments = new List<Appointment>();

        }

        public Patient() { }
   }
}