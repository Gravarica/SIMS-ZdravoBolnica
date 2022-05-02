using System;
using System.Collections.Generic;
using System.Linq;
using HospitalProject.FileHandler;
using HospitalProject.Model;
using Model;
using Repository;

namespace HospitalProject.Repository;

public class AllergiesRepository
{
      
        private List<Allergies> _allergies;
        private AllergiesFileHandler _allergiesFileHandler;
        private PatientFileHandler _patientFileHandler;
        private int _allergiesMaxId;  
        private AllergiesRepository _allergiesRepository;
        public PatientRepository _patientRepository;
        public MedicalRecordRepository _medicalRecordRepository;
        
        public AllergiesRepository(AllergiesFileHandler allergiesFileHandler, AllergiesRepository allergiesRepository)
        {
            _allergiesFileHandler = allergiesFileHandler;
            _allergiesRepository = allergiesRepository;
            _allergies = _allergiesFileHandler.ReadAll().ToList();
            _allergiesMaxId = GetMaxId();
            LinkAllergiesWithPatients();
        }
        private int GetMaxId() {
            return _allergies.Count() == 0 ? 0 : _allergies.Max(Allergy => Allergy.Id);
        }
        
        public void Insert(Allergies a)
        {
            a.Id = ++_allergiesMaxId;
            _allergies.ToList().Add(a);
            _allergiesFileHandler.AppendLineToFile(a);
        }

        public Allergies GetById(int id)
        {
            _allergies = (List<Allergies>)_allergiesFileHandler.ReadAll();
            foreach (Allergies allergy in _allergies)
            {
                if (allergy.Id==id)
                {
                    return allergy;
                }
            }
            return null;
        }

        public List<Allergies> GetAll()
        {
         return _allergies;
        }

        public void Update(Allergies a)
        {
            _allergies = (List<Allergies>)_allergiesFileHandler.ReadAll();
            foreach (Allergies allergy in _allergies)
            {
                if (allergy.Id == a.Id)
                {
                    allergy.Name = a.Name;
                    _allergiesFileHandler.Save(_allergies);
                   
                }
            }
           
        }

        

        public Boolean Delete(int id)
        {
            _allergies = (List<Allergies>)_allergiesFileHandler.ReadAll();
            foreach (Allergies allergy in _allergies)
            {
                if (allergy.Id==id)
                {
                    _allergies.Remove(allergy);
                    _allergiesFileHandler.Save(_allergies);
                    return true;
                }
            }
            return false;
        }
        
        private void LinkAllergiesWithPatients()
        { 
        

            foreach (Allergies allergy in _allergies)
            {
                foreach (int id in allergy.PatientsID)
                {
                   Patient p = _patientRepository.GetById(id);
                   
                   _medicalRecordRepository.AddNewAllergiesToMedicalRecord(allergy, p.Id);
                
                }

            }
        }
        
        public List<Allergies> GetAllergiesByMedicalRecord(int patientId)
        {
            List<Allergies> allergiesReturnList = new List<Allergies>();
            
//pronadjemo pacijenta po ID 
            //_allergies = (List<Allergies>) _allergiesFileHandler.ReadAll();
            IEnumerable<Patient> patients = _patientFileHandler.ReadAll();
            
            Patient p = patients.SingleOrDefault(r => r.Id.Equals(patientId));
            int s = p.MedicalRecordId;
           
            MedicalRecord MedRec = _medicalRecordRepository.GetById(s);
            
            foreach(Allergies allergy in MedRec.Allergies)
            {
                allergiesReturnList.Add(allergy);
            }

            return allergiesReturnList;
        }
        
        public List<Patient> GetPatientsByAllergy(int allergyID)
        {
            List<Patient> patientsReturnList = new List<Patient>();
            
//pronadjemo alergiju po ID 
            _allergies = (List<Allergies>) _allergiesFileHandler.ReadAll();
            Allergies a = _allergies.SingleOrDefault(r => r.Id.Equals(allergyID));
            
            foreach(int patientID in a.PatientsID)
            {
                Patient patient = _patientRepository.GetById(patientID);
                patientsReturnList.Add(patient);
            }

            return patientsReturnList;
        }
}
