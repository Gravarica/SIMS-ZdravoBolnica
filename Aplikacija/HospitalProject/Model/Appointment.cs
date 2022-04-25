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
      private int doctorID;
      private int patientID;
      private int roomID;
      private int id;
        private bool isDone;
      
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
            if (this.room == null || !this.room.Equals(value))
            {
               if (this.room != null)
               {
                  Room oldRoom = this.room;
                  this.room = null;
                  oldRoom.RemoveAppointments(this);
               }
               if (value != null)
               {
                  this.room = value;
                  this.room.AddAppointments(this);
               }
            }
         }
      }

        public int Id { get; set; }

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

      public int DoctorId { get; set; }

      public int PatientId{ get; set; }

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

        public int RoomID { get; set; }

        public Appointment() { }

        public Appointment(int id, DateTime date, int duration, int patientId, int doctorId, int roomId)
        {
            Id = id;
            Date = date;
            Patient = new Patient();
            Doctor = new Doctor();
            Room = new Room();
            Duration = duration;
            Patient.Id = patientId;
            Doctor.Id = doctorId;
            Room.Id = roomId;
        }

        public Appointment(DateTime date, int duration, Doctor doctor, Patient patient)
        {
            Date = date;
            Duration = duration;
            Patient = patient;
            Doctor = doctor;
            RoomID = 5;
        }

        public override bool Equals(object? obj)
        {
            return obj is Appointment appointment &&
                   Date == appointment.Date;
        }
    }
}