using HospitalProject.Model;
using HospitalProject.Repository;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class MedicalRecordService
    {
        private PatientService _patientService;
        private AnamnesisService _anamnesisService;
        private AllergiesService _allergiesService;
        private MedicalRecordRepository _medicalRecordRepostiory;

       

        public MedicalRecordService(AllergiesService allergiesService,  AnamnesisService anamnesisService, MedicalRecordRepository medicalRecordRepostiory, PatientService patientService)
        {
            _allergiesService = allergiesService;
            _anamnesisService=anamnesisService;
            _medicalRecordRepostiory=medicalRecordRepostiory;
            _patientService=patientService;
        }

        // Creates a new medical record
        public MedicalRecord Create(MedicalRecord medicalRecord)
        {
            medicalRecord.Id = _medicalRecordRepostiory.GetMaxId();
            _patientService.SetPatientMedicalRecord(medicalRecord.Patient.Id, medicalRecord.Id);
            return _medicalRecordRepostiory.Insert(medicalRecord);
        }

        public MedicalRecord Create(MedicalRecord medicalRecord, Patient patient)
        {
            SetPatientMedicalRecord(medicalRecord, patient);
            _medicalRecordRepostiory.Insert(medicalRecord);
            return medicalRecord;
        }

        private MedicalRecord SetPatientMedicalRecord(MedicalRecord medicalRecord, Patient patient)
        {
            medicalRecord.Id = _medicalRecordRepostiory.GetMaxId() + 1;
            medicalRecord.Patient = patient;
            return medicalRecord;
        }


        // Deletes a medical record by a given id
        public void Delete(int id)
        {
            _medicalRecordRepostiory.Delete(id);
        }

        // Returns all medical records in the system
        public List<MedicalRecord> GetAll()
        {
            BindMedicalRecordsWithPatients();
            BindMedicalRecordsWithAnamneses();
            return _medicalRecordRepostiory.GetAll();
        }

        // Returns Medical Record by given id
        public MedicalRecord GetById(int id)
        {
            MedicalRecord retMedRecord = _medicalRecordRepostiory.GetById(id);
            BindMedicalRecordWithPatient(retMedRecord);
            BindMedicalRecordWithAnamneses(retMedRecord);
            return retMedRecord;
        }

        // Returns Medical Record by given patient
        public MedicalRecord GetByPatient(Patient patient)
        {
            return GetById(patient.MedicalRecordId);
        }

        // Updates medical record, for now it's only set to update patient data
        public void Update(MedicalRecord medicalRecord)
        {
            _medicalRecordRepostiory.Update(medicalRecord);
        }

        // For each medical record there is, instatiates its patient by read patient id from file
        private void BindMedicalRecordsWithPatients()
        {

            foreach(MedicalRecord record in _medicalRecordRepostiory.GetAll())
            {
                record.Patient = _patientService.GetById(record.Patient.Id);
            }
        }

        // For each medical record there is, instatiates its list of anamneses with objects
        private void BindMedicalRecordsWithAnamneses()
        {

            foreach (MedicalRecord record in _medicalRecordRepostiory.GetAll())
            {
                record.Anamneses = _anamnesisService.GetAnamnesesByMedicalRecord(record.Patient.Id);
            }

        }

        // For given medical record, instantiates its patient by read patient id from file
        private void BindMedicalRecordWithPatient(MedicalRecord medicalRecord)
        {
            medicalRecord.Patient = _patientService.GetById(medicalRecord.Patient.Id);
        }

        // For given medical record, instantiates its list of anamneses with objects 
        private void BindMedicalRecordWithAnamneses(MedicalRecord medicalRecord)
        {
            medicalRecord.Anamneses = _anamnesisService.GetAnamnesesByMedicalRecord(medicalRecord.Patient.Id);
        }

        public void AddNewAnamnesisToMedicalRecord(Anamnesis anamnesis)
        {
            _medicalRecordRepostiory.AddNewAnamnesisToMedicalRecord(anamnesis);
        }
        public void AddNewAllergiesToMedicalRecord(Allergies allergies, Patient patient)
        {
            _medicalRecordRepostiory.AddNewAllergiesToMedicalRecord(allergies, patient);
        }

    }
}
