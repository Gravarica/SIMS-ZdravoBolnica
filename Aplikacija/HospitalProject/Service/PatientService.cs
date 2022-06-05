
using System;
using Repository;
using Model;
using System.Collections.Generic;
using HospitalProject.Service;

namespace Service
{
   public class PatientService
   {
        private PatientRepository _patientRepository;
        public PatientService(PatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }

        public Patient Get(int id)

      {
         return _patientRepository.Get(id);
      }
      
      public IEnumerable<Patient> GetAll()
      {
         return _patientRepository._patientFileHandler.ReadAll();
      }

      public void Update(Patient patient)
      {
          _patientRepository.Update(patient);
      }

        public void Delete(int id) => _patientRepository.Delete(id);

        public Patient Create(Patient patient)
        {
            return _patientRepository.Insert(patient);
        }

        public void SetPatientMedicalRecord(int patientId, int medicalRecordId)
        {
            _patientRepository.GetById(patientId).MedicalRecordId = medicalRecordId;
        }
        
        
        public Patient GetById(int id)
        {
            return _patientRepository.GetById(id);
        }
        

        public Patient GetLoggedPatient(string username)
        {
            return _patientRepository.GetLoggedPatient(username);
        }

    }

    

}