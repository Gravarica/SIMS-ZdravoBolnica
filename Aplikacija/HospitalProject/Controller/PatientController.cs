// File:    PatientController.cs
// Author:  aleksa
// Created: Monday, April 4, 2022 18:46:10
// Purpose: Definition of Class PatientController

using System;
using Service;
using Model;
using System.Collections.Generic;
using HospitalProject.Service;
using HospitalProject;

namespace Controller
{
   public class PatientController
   {
      private PatientService _patientService;
      private UserService userService;
      
      public PatientController(PatientService patientService, UserService userService)
        {
            var app = System.Windows.Application.Current as App;
            _patientService = patientService;
            this.userService = userService;
        }


        public Patient Create(Patient patient)
        {
           userService.Create(new User(patient.Username, patient.Password, UserType.PATIENT));
           return _patientService.Create(patient);
           
        }


        public Patient Get(int id)
      {
         return _patientService.Get(id);
      }
      
      public IEnumerable<Patient> GetAll()
      {
         return _patientService.GetAll();
      }
      
      public void Delete(int id)
      {
            //userService.Delete(username)
            _patientService.Delete(id);
        }
      
      public void Update(Patient patient)
      {
            _patientService.Update(patient);
        }

        public Patient GetLoggedPatient(string username)
        {
            return _patientService.GetLoggedPatient(username);
        }



    }
}