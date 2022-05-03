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
        private MedicalRecordFileHandler _medicalRecordFileHandler;
        private int _allergiesMaxId;

        public PatientRepository _patientRepository;
        public MedicalRecordRepository _medicalRecordRepository;
        
        public AllergiesRepository(AllergiesFileHandler allergiesFileHandler)
        {
            _allergiesFileHandler = allergiesFileHandler;
            
            _allergies = _allergiesFileHandler.ReadAll().ToList();
            _allergiesMaxId = GetMaxId();

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

    //dpbavi alergije po ID
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

    public List<Allergies> GetAllergiesByMedicalRecord(int medicalRecordId)
    {
        List<Allergies> allergiesReturnList = new List<Allergies>();

        //pronadjemo pacijenta po ID 
        IEnumerable<MedicalRecord> Mrecords = _medicalRecordFileHandler.ReadAll();

        MedicalRecord m = Mrecords.SingleOrDefault(r => r.Id.Equals(medicalRecordId));

        foreach (Allergies allergy in m.Allergies)
        {
            allergiesReturnList.Add(allergy);
        }

        return allergiesReturnList;
    }


    public List<Allergies> GetAllergiesByPatientID(int patientId)
        {
            List<Allergies> allergiesReturnList = new List<Allergies>();
            
//pronadjemo pacijenta po ID 
            IEnumerable<Patient> patients = _patientFileHandler.ReadAll();
            
            Patient p = patients.SingleOrDefault(r => r.Id.Equals(patientId));
            int s = p.MedicalRecordId;
           
            MedicalRecord MedRec = _medicalRecordRepository.GetById(s);
            
            foreach(Allergies allergy in MedRec.Allergies)
            {
                allergiesReturnList.Add(allergy);
              //   allergy.PatientsID.Add(p.Id);
            }

            return allergiesReturnList;
        }
        
    
}
