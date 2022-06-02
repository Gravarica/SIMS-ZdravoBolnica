// File:    Room.cs
// Author:  aleksa
// Created: Sunday, March 27, 2022 18:59:11
// Purpose: Definition of Class Room

using System;
using System.Collections.Generic;
using System.Windows.Documents;
using HospitalProject.Model;
using HospitalProject.View.WardenForms.ViewModels;

namespace Model
{
   public class Room
   {
      public int _id { get; set; }
      public int _number{ get; set; }
      public int _floor{ get; set; }
      public RoomType _roomType { get; set; }
      
      private System.Collections.Generic.List<Equipement> equipment;

      public Room(int number, int floor, RoomType roomType)
      {
         _number = number;
         _floor = floor;
         _roomType = roomType;
         Equipment = new List<Equipement>();
         Appointments = new List<Appointment>();
      }

      public Room(List<Equipement> equipment, int id, int number, int floor, RoomType roomType)
      {
         this.equipment = equipment;
         _id = id;
         _number = number;
         _floor = floor;
         _roomType = roomType;
      }

      private System.Collections.Generic.List<Appointment> appointments;
      
      
      

        public Room(int id, int n, int f,RoomType rt) { 
            _id = id;
            _number = n;
            _floor = f;
            _roomType = rt;
        }

        public Room()
        {
        }

        public Room(int id)
        {
           _id = id;
        }


        public Room(int id, int number)
        {
           _id = id;
           _number = number;
        }

        public System.Collections.Generic.List<Equipement> Equipment
        {
           get
           {
              if (equipment == null)
              {
                 equipment = new System.Collections.Generic.List<Equipement>();
              }
              return equipment;
           }
           set
           {
              if (equipment != null)
              {
                 equipment.Clear();
              }
              if (value != null)
              {
                 foreach (Equipement eq in value)
                 {
                    AddEquipment(eq);
                 }
              }
              
           }
        }
        

        /// <summary>
      /// Property for collection of Appointment
      /// </summary>
      /// <pdGenerated>Default opposite class collection property</pdGenerated>
      public System.Collections.Generic.List<Appointment> Appointments
      {
         get
         {
            if (appointments == null)
               appointments = new System.Collections.Generic.List<Appointment>();
            return appointments;
         }
         set
         {
            RemoveAllAppointments();
            if (value != null)
            {
               foreach (Appointment oAppointment in value)
                  AddAppointments(oAppointment);
            }
         }
      }

        public void AddEquipment(Equipement eq)
        {
           if (eq == null)
           {
              return;
           }

           if (equipment == null)
           {
              equipment = new System.Collections.Generic.List<Equipement>();
           }

           if (!equipment.Contains(eq))
           {
              equipment.Add(eq);
           }
           
        }
      
      /// <summary>
      /// Add a new Appointment in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddAppointments(Appointment newAppointment)
      {
         if (newAppointment == null)
            return;
         if (this.appointments == null)
            this.appointments = new System.Collections.Generic.List<Appointment>();
         if (!this.appointments.Contains(newAppointment))
         {
            this.appointments.Add(newAppointment);
            newAppointment.Room = this;
         }
      }
      
      /// <summary>
      /// Remove an existing Appointment from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveAppointments(Appointment oldAppointment)
      {
         if (oldAppointment == null)
            return;
         if (this.appointments != null)
            if (this.appointments.Contains(oldAppointment))
            {
               this.appointments.Remove(oldAppointment);
               oldAppointment.Room = null;
            }
      }
      
      /// <summary>
      /// Remove all instances of Appointment from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllAppointments()
      {
         if (appointments != null)
         {
            System.Collections.ArrayList tmpAppointments = new System.Collections.ArrayList();
            foreach (Appointment oldAppointment in appointments)
               tmpAppointments.Add(oldAppointment);
            appointments.Clear();
            foreach (Appointment oldAppointment in tmpAppointments)
               oldAppointment.Room = null;
            tmpAppointments.Clear();
         }
      }

        
        public int Id 
        { 
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public int Number 
        { 
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }

        public int Floor { get; set; }

        public RoomType RoomType
        {
            get; set;
        }

        public Room(RoomCheckBoxModel rcbm)
        {
           _id = rcbm.Id;
           _floor = rcbm.Floor;
           _number = rcbm.Number;
           Equipment = rcbm.Equipement;
           RoomType =(RoomType) Enum.Parse(typeof(RoomType), rcbm.RoomType, true);
        }
   }
}