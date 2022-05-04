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
using HospitalProject.Model;
using System.Linq;

namespace Controller
{
   public class PatientController
   {
      private PatientService _patientService;
      private UserService userService;
      private MedicalRecordService _medicalRecordService;
      private int id;
        public PatientController(PatientService patientService, UserService userService, MedicalRecordService medicalRecordService)
        {

            var app = System.Windows.Application.Current as App;
            _patientService = patientService;
            _medicalRecordService = medicalRecordService;
            this.userService = userService;
        }


        public Patient Create(Patient patient)
        {
           userService.Create(new User(patient.Username, patient.Password, UserType.PATIENT));
          // MedicalRecord NewPatientMR = new MedicalRecord(id, patient.Id);
          // _medicalRecordService.Create(NewPatientMR);
            
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
            Patient P = Get(id);

            userService.Delete(P.Username);
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