using HospitalProject.FileHandler;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace HospitalProject.Repository
{
    public class MedicalRecordRepository
    {
        private MedicalRecordFileHandler _medicalRecordFileHandler;
        private AllergiesRepository _allergiesRepository;
        private PatientFileHandler _patientFileHandler;
        private List<MedicalRecord> _medicalRecords;

        private int _medicalRecordMaxId;

        public MedicalRecordRepository(MedicalRecordFileHandler medicalRecordFileHandler, AllergiesRepository allergiesRepository)
        {
            InstantiateLayers(medicalRecordFileHandler, allergiesRepository);
            InstantiateData();
        }

        private void InstantiateLayers(MedicalRecordFileHandler medicalRecordFileHandler, AllergiesRepository allergiesRepository)
        {
            _medicalRecordFileHandler=medicalRecordFileHandler;

            _allergiesRepository = allergiesRepository;
        }

        private void InstantiateData()
        {
            _medicalRecords = _medicalRecordFileHandler.ReadAll().ToList();

            BindAllergensForMedicalRecord();

            _medicalRecordMaxId = GetMaxId();
        }

        public int GetMaxId()
        {
            return _medicalRecords.Count() == 0 ? 0 : _medicalRecords.Max(appointment => appointment.Id);
        }

        public List<MedicalRecord> GetAll()
        {
            return _medicalRecords;
        }

        public MedicalRecord GetById(int id)
        {
            return _medicalRecords.FirstOrDefault(x => x.Id == id);
        }
        
        public MedicalRecord Insert(MedicalRecord medicalRecord)
        {
            _medicalRecordMaxId++;
            _medicalRecordFileHandler.AppendLineToFile(medicalRecord);
            return medicalRecord;
        }

        public void Delete(int id)
        {
            MedicalRecord deleteMedicalRecord = GetById(id);
            _medicalRecords.Remove(deleteMedicalRecord);
            _medicalRecordFileHandler.Save(_medicalRecords);
        }

        public void Update(MedicalRecord medicalRecord)
        {
            MedicalRecord updateMedicalRecord = GetById(medicalRecord.Id);
            updateMedicalRecord.Patient = medicalRecord.Patient;
        }

        public void AddNewAnamnesisToMedicalRecord(Anamnesis anamnesis)
        {
            MedicalRecord updateMedicalRecord = GetById(anamnesis.App.Patient.MedicalRecordId);
            updateMedicalRecord.Anamneses.Add(anamnesis);
            //_medicalRecordFileHandler.Save(_medicalRecords);
        }
        
        public void AddNewAllergiesToMedicalRecord(Allergies allergies, int PatientID)
        {   
            List<Patient> patients = _patientFileHandler.ReadAll().ToList();

            Patient p = patients.SingleOrDefault(r => r.Id.Equals(PatientID));
            int s = p.MedicalRecordId;
            MedicalRecord updateMedicalRecord = GetById(s);
            updateMedicalRecord.Allergies.Add(allergies);
          
        }

        private void BindAllergensForMedicalRecord()
        {
            _medicalRecords.ForEach(medicalRecord => SetAllergiesForMedicalRecord(medicalRecord));
        }

        private void SetAllergiesForMedicalRecord(MedicalRecord medicalRecord)
        {
            foreach (Allergies allergy in medicalRecord.Allergies)
            {
                SetAllergen(allergy);
            }
        }

        private void SetAllergen(Allergies allergy)
        {
            allergy.Name = _allergiesRepository.GetById(allergy.Id).Name;
        }


    }
}
