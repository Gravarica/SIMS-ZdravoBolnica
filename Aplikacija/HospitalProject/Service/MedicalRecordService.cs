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

        private AnamnesisService _anamnesisService;

        private MedicalRecordRepository _medicalRecordRepostiory;

        private PatientService _patientService;

        public MedicalRecordService(AnamnesisService anamnesisService, MedicalRecordRepository medicalRecordRepostiory, PatientService patientService)
        {

            _anamnesisService=anamnesisService;

            _medicalRecordRepostiory=medicalRecordRepostiory;

            _patientService=patientService;
        }

        public MedicalRecord Create(MedicalRecord medicalRecord)
        {
            return _medicalRecordRepostiory.Insert(medicalRecord);
        }

        public void Delete(int id)
        {
            _medicalRecordRepostiory.Delete(id);
        }

        public List<MedicalRecord> GetAll()
        {
            BindMedicalRecordsWithPatients();
            BindMedicalRecordsWithAnamneses();
            return _medicalRecordRepostiory.GetAll();
        }

        public MedicalRecord GetById(int id)
        {
            return _medicalRecordRepostiory.GetById(id);
        }

        public void Update(MedicalRecord medicalRecord)
        {
            _medicalRecordRepostiory.Update(medicalRecord);
        }

        private void BindMedicalRecordsWithPatients()
        {

            foreach(MedicalRecord record in _medicalRecordRepostiory.GetAll())
            {
                record.Patient = _patientService.GetById(record.Patient.Id);
            }
        }

        private void BindMedicalRecordsWithAnamneses()
        {

            foreach (MedicalRecord record in _medicalRecordRepostiory.GetAll())
            {
                record.Anamneses = _anamnesisService.GetAnamnesesByMedicalRecord(record.Patient.Id);
            }

        }

    }
}
