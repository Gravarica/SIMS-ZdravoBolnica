
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Exception;
using HospitalProject.FileHandler;
using HospitalProject.Model;
using HospitalProject.Repository;
using Model;

namespace Repository
{
   public class PatientRepository
   {
        private const string NOT_FOUND_ERROR = "Account with {0}:{1} can not be found!";
        private string _path;       // Putanja do fajla 
        private string _delimiter;  // Delimiter za CSV format
        private int _patientMaxId;
        public PatientFileHandler _patientFileHandler;
        private MedicalRecordRepository _medicalRecordRepository;
        private List<Patient> _patients;

        public PatientRepository(PatientFileHandler patientFileHandler, MedicalRecordRepository medicalRecordRepository)
        {
            _patientFileHandler = patientFileHandler;
            _medicalRecordRepository = medicalRecordRepository;
            _patients =  _patientFileHandler.ReadAll().ToList();
            _patientMaxId = GetMaxId(patients: _patientFileHandler.ReadAll());
        }

        private int GetMaxId(IEnumerable<Patient> patients)
        {
            return !patients.Any() ? 0 : patients.Max(patient => patient.Id);
        }

        public Patient GetById(int id)
        {
            return _patients.FirstOrDefault(patient => patient.Id == id);
        }

        public Patient Insert(Patient patient)
        {
            patient.Id = ++_patientMaxId;
            patient.MedicalRecordId = _medicalRecordRepository.GetMaxId() + 1;
            _patientFileHandler.AppendLineToFile(patient);
            return patient;
        }
        
        public void Update(Patient patient)
        {

            Patient updatedPatient = GetById(patient.Id);

            updatedPatient.MedicalRecordId = patient.MedicalRecordId;
            updatedPatient.Guest = patient.Guest;
            updatedPatient.Username = patient.Username;
            updatedPatient.FirstName = patient.FirstName;
            updatedPatient.LastName = patient.LastName;
            updatedPatient.Jmbg = patient.Jmbg;
            updatedPatient.PhoneNumber = patient.PhoneNumber;
            updatedPatient.Email = patient.Email;
            updatedPatient.Adress = patient.Adress;
            updatedPatient.DateOfBirth = patient.DateOfBirth;
            updatedPatient.Gender = patient.Gender;
            
            _patientFileHandler.Save();
        }
        public void Delete(int id)
        {
            Patient patient = _patients.SingleOrDefault(r => r.Id.Equals(id));

            if (patient != null)
            {
                _patients.Remove(patient);
                _patientFileHandler.Save();
            }

        }

        
        // Pokupi samo jedan entitet po njegovom ID-u
        public Patient Get(int id)
        {
            try
            {
                {                                                                           // Vraca ili onaj entitet koji je jedinstven, ili vraca default vrednost
                    return _patientFileHandler.ReadAll().SingleOrDefault(patient => patient.Id == id);           // Daj mi onaj account za koji je account.Id == id
                }
            }
            catch (ArgumentException)
            {
                {
                    throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
                }
            }
        }

        public Patient GetLoggedPatient(string username)
        {
            try
            {
                {
                    return GetAll().SingleOrDefault(patient => patient.Username.Equals(username));
                }
            }
            catch (ArgumentException)
            {
                {
                    throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "username", username));
                }
            }
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patients;
        }
    }
}