// File:    Doctor.cs
// Author:  aleksa
// Created: Sunday, March 27, 2022 18:27:47
// Purpose: Definition of Class Doctor

using System;
using System.Collections.Generic;

namespace Model
{
   public class Doctor : User
   {

        private int id;
     
        private List<Appointment> appointments;
        private TimeOnly shiftStart;
        private TimeOnly shiftEnd;

        public List<Appointment> Appointments 
        { 
            get 
            { 
                return appointments; 
            }
            set
            {
                appointments = value;
            }
        }

        public TimeOnly ShiftStart
        { 
            get 
            { 
                return shiftStart;
            }
            set
            {
                shiftStart = value;
                OnPropertyChanged(nameof(ShiftStart));
            }
        }

        public TimeOnly ShiftEnd
        {
            get
            {
                return shiftEnd;
            }
            set
            {
                shiftEnd = value;
                OnPropertyChanged(nameof(ShiftEnd));
            }
        }


        public int Id {
            get {
                return id;
            }
            set 
            {
                id = value;            
            } 
        }
   
        public Doctor(int id, String username, String password, string lastName, TimeOnly shiftStart, TimeOnly shiftEnd) : base(username,password,lastName)
        {
            this.id = id;
            appointments = new List<Appointment>();
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;    
        }

        public Doctor(int id) : base()
        {
            Id = id;
            appointments = new List<Appointment>();    
        }

        public Doctor() : base (){ }
   }
}