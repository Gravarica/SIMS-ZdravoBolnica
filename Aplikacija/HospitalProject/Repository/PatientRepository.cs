
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Exception;
using HospitalProject.FileHandler;
using Model;

namespace Repository
{
   public class PatientRepository
   {
        private const string NOT_FOUND_ERROR = "Account with {0}:{1} can not be found!";
        private string _path;       // Putanja do fajla 
        private string _delimiter;  // Delimiter za CSV format
        private int _patientMaxId;
        private int _patientMRMaxId;
        public PatientFileHandler _patientFileHandler;

        private List<Patient> _patients = new List<Patient>();

        public PatientRepository(PatientFileHandler patientFileHandler)
        {
            _patientFileHandler = patientFileHandler;
            _patients = (List<Patient>) _patientFileHandler.ReadAll();
             _patientMaxId = GetMaxId(patients: _patientFileHandler.ReadAll());
            _patientMRMaxId = GetMaxMRId(patients: _patientFileHandler.ReadAll());

        }

        private int GetMaxId(IEnumerable<Patient> patients)
        {
            return patients.Count() == 0 ? 0 : patients.Max(patient => patient.Id);
        }

        private int GetMaxMRId(IEnumerable<Patient> patients)
        {
            return patients.Count() == 0 ? 0 : patients.Max(patient => patient.MedicalRecordId);
        }

        public List<Patient> Patients
        { get { return _patients; } }

       

        public Patient GetById(int id)
        {
            return _patients.FirstOrDefault(p => p.Id == id);
        }

        public Patient Insert(Patient patient)
        {
            patient.Id = ++_patientMaxId;
            patient.MedicalRecordId = ++_patientMRMaxId;
            _patientFileHandler.AppendLineToFile(patient);
            return patient;
        }
        
        public void Update(Patient patient)
        {
            foreach (Patient p in _patients)
            {
                if (patient.Id == p.Id)
                {
                    p.DateOfBirth = patient.DateOfBirth;
                    p.BloodType = patient.BloodType;
                    p.Password = patient.Password;
                    p.Gender = patient.Gender;
                    p.Guest = patient.Guest;
                    p.Email = patient.Email;
                    p.Adress = patient.Adress;
                    p.Jmbg = patient.Jmbg;
                    p.PhoneNumber = patient.PhoneNumber;
                    p.FirstName = patient.FirstName;
                    p.LastName = patient.LastName;
                    p.Username = patient.Username;
                    break;
                }
            }

            _patientFileHandler.Save();
        }
        public void Delete(int id)
        {
            List<Patient> patients = _patientFileHandler.ReadAll().ToList();

            Patient p = patients.SingleOrDefault(r => r.Id.Equals(id));

            if (p != null)
            {
                patients.Remove(p);
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