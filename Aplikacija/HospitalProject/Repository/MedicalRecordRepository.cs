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
        private IHandleData<MedicalRecord> _medicalRecordFileHandler;
        private AllergiesRepository _allergiesRepository;
        private List<MedicalRecord> _medicalRecords;

        private int _medicalRecordMaxId;

        public MedicalRecordRepository(AllergiesRepository allergiesRepository)
        {
            InstantiateLayers(allergiesRepository);
            InstantiateData();
        }

        private void InstantiateLayers(AllergiesRepository allergiesRepository)
        {
            _medicalRecordFileHandler=new MedicalRecordFileHandler(FilePathStorage.MEDICALRECORD_FILE);

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
            _medicalRecordFileHandler.SaveOneEntity(medicalRecord);
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
        
        public void AddNewAllergiesToMedicalRecord(Allergies allergy, Patient patient)
        {   
            MedicalRecord updateMedicalRecord = GetById(patient.MedicalRecordId);
            updateMedicalRecord.Allergies.Add(allergy);
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
