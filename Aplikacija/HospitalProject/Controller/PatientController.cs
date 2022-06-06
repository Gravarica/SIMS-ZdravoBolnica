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
       private UserService _userService;
       private AppointmentService _appointmentService;
       private MedicalRecordService _medicalRecordService;
      
        public PatientController(PatientService patientService, UserService userService, MedicalRecordService medicalRecordService, AppointmentService appointmentService)
        {

            var app = System.Windows.Application.Current as App;
            
            _patientService = patientService;
            _medicalRecordService = medicalRecordService;
            this._userService = userService;
            this._appointmentService=appointmentService;
        }


        public Patient Create(Patient patient)
        {
            CreateNewUser(patient);
            MedicalRecord NewMedicalRecord = SetPatientMedicalRecord(patient,new MedicalRecord(0, patient.Id));
            return patient;
        }

        private Patient CreateNewUser(Patient patient)
        {
            _userService.Create(new User(patient.Username, patient.Password, UserType.PATIENT, false, 0));
            _patientService.Create(patient);
            return patient;
        }
        

        private MedicalRecord SetPatientMedicalRecord(Patient patient, MedicalRecord medicalRecord)
        {
            _medicalRecordService.Create(medicalRecord, patient);
            return medicalRecord;
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
            Patient Patient = Get(id);

            _userService.Delete(Patient.Username);
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

        public List<Patient> GetPatientsThatHadAppointmentWithDoctor(Doctor doctor)
        {
            return _appointmentService.GetAllPatientsThatHadAppointmentWithDoctor(doctor);
        }

    }
}