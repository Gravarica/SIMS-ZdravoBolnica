// File:    Appointment.cs
// Author:  aleksa
// Created: Sunday, March 27, 2022 18:54:07
// Purpose: Definition of Class Appointment

using HospitalProject.Model;
using System;

namespace Model
{
   public class Appointment : ViewModelBase
   {
      private DateTime date;
      private int duration;
      private int id;
      private bool isDone;
      private ExaminationType examinationType;
      private Patient patient;
      private Doctor doctor;
      private Room room;
      
      public Room Room
      {
         get
         {
            return room;
         }
         set
            {
                room = value;
                OnPropertyChanged(nameof(Room));  
            }
      }

      public int Id 
      { 
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
      }

      public DateTime Date 
        { 
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

      public int Duration 
        { 
            get
            {
                return duration;
            }
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

      public Patient Patient 
      {
          get { 
              return patient;
          }
          set {
              patient = value;
                OnPropertyChanged(nameof(Patient));
          }
      }

      public Doctor Doctor
      {
            get {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged(nameof(Doctor));
            }
        }

        public bool IsDone
        {
            get
            {
                return isDone;
            }
            set
            {
                isDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }

        public ExaminationType ExaminationType
        {
            get
            {
                return examinationType;
            }
            set
            {
                examinationType = value;
                OnPropertyChanged(nameof(ExaminationType));
            }
        }

        public Appointment() { }

        // Constructor that is used for reading from file
        public Appointment(int id, DateTime date, int duration, int patientId, int doctorId, int roomId, ExaminationType examinationType, bool isDone)
        {
            Id = id;
            SetIds(patientId, doctorId, roomId);
            SetFields(duration, examinationType, isDone, date);
        }

        // Constructor that is used for creating a new appomitment
        public Appointment(DateTime date, int duration, Doctor doctor, Patient patient, Room room, ExaminationType examinationType)
        {
            Patient = patient;
            Doctor = doctor;
            Room = room;
            SetFields(duration, examinationType, false, date); 
        }

        // Method that sets field values for an appointment
        private void SetFields(int duration, ExaminationType examinationType, bool isDone, DateTime date)
        {
            Date = date;
            Duration = duration;
            ExaminationType = examinationType;
            IsDone = isDone;
        }

        // Method that is used for reading from a file
        private void SetIds(int patientId, int doctorId, int roomId)
        {
            InstantiateEmptyObjects();
            Patient.Id = patientId;
            Doctor.Id = doctorId;
            Room.Id = roomId;
        }

        private void InstantiateEmptyObjects()
        {
            Patient = new Patient();
            Doctor = new Doctor();
            Room = new Room();
        }

        public override bool Equals(object? obj)
        {
            return obj is Appointment appointment &&
                   Date == appointment.Date;
        }
    }
}