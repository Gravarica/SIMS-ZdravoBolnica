// File:    Appointment.cs
// Author:  aleksa
// Created: Sunday, March 27, 2022 18:54:07
// Purpose: Definition of Class Appointment

using System;

namespace Model
{
   public class Appointment
   {
      private DateTime date;
      private int duration;
      private int doctorID;
      private int patientID;
      private int roomID;
      private int id;
      
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

        public DateTime Date { get; set; }

      public int Duration { get; set; }

      public int DoctorId { get; set; }

      public int PatientId{ get; set; }

      public Patient Patient 
      {
          get { 
              return patient;
          }
          set {
              patient = value;
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
            }
        }

        public int RoomID { get; set; }

        public Appointment() { }

        public Appointment(int id, DateTime date, int duration, int patientId, int doctorId)
        {
            Id = id;
            Date = date;
            Duration = duration;
            PatientId = patientId;
            DoctorId = doctorId; 
            RoomID = 5;
        }

        public Appointment(DateTime date, int duration, Doctor doctor, Patient patient)
        {
            Date = date;
            Duration = duration;
            Patient = patient;
            Doctor = doctor;
            PatientId = Patient.Id;
            DoctorId = Doctor.Id;
            RoomID = 5;
        }

        public override bool Equals(object? obj)
        {
            return obj is Appointment appointment &&
                   Date == appointment.Date;
        }
    }
}