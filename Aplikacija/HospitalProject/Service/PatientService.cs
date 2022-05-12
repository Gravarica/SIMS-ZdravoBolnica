
using System;
using Repository;
using Model;
using System.Collections.Generic;

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

        public Patient GetByJmbg(int jmbg)

        {
            return _patientRepository.Get(jmbg);
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


        public Patient GetById(int id)
        {
            return _patientRepository.GetById(id);
        }

        public void SetPatientMedicalRecord(int patientId, int medicalRecordId)
        {
            GetById(patientId).MedicalRecordId = medicalRecordId;
        }

        public Patient GetLoggedPatient(string username)
        {
            return _patientRepository.GetLoggedPatient(username);
        }

    }

    

}