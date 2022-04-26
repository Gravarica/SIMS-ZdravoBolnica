// File:    PatientController.cs
// Author:  aleksa
// Created: Monday, April 4, 2022 18:46:10
// Purpose: Definition of Class PatientController

using System;
using Service;
using Model;
using System.Collections.Generic;

namespace Controller
{
   public class PatientController
   {
      private PatientService _patientService;
      
      public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }


        public Patient Create(Patient patient)
        {
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
      
      public void Delete(string id)
      {
            _patientService.Delete(id);
        }
      
      public void Update(Patient patient)
      {
            _patientService.Update(patient);
        }
   
   }
}