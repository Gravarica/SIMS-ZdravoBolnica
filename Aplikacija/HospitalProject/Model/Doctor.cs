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

        
        public int Id {
            get {
                return id;
            }
            set 
            {
                id = value;            
            } 
        }
   
        public Doctor(int id, String username, String password, string lastName) : base(username,password,lastName)
        {
            this.id = id;
            appointments = new List<Appointment>();
        }

        public Doctor(int id) : base()
        {
            Id = id;
            appointments = new List<Appointment>();    
        }
   }
}