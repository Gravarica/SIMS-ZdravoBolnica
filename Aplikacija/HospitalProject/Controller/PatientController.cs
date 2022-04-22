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


        /*private void Create(string id, string name, string surname, string mail, string adress, DateTime dateOfBirth)
        {
           _patientService.Create(id, name, surname, mail, adress, dateOfBirth);
        }*/

        public Patient Add(Patient patient)
        {
            return _patientService.Add(patient);
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