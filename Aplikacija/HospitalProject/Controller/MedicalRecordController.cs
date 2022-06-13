using HospitalProject.Model;
using HospitalProject.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class MedicalRecordController
    {

        private MedicalRecordService _medicalRecordService;

        public MedicalRecordController(MedicalRecordService medicalRecordService)
        {
            _medicalRecordService=medicalRecordService;
        }

        public void Create(MedicalRecord medicalRecord)
        {
            _medicalRecordService.Create(medicalRecord);
        }

        public void Delete(int id)
        {
            _medicalRecordService.Delete(id);
        }

        public void Update(MedicalRecord medicalRecord)
        {
            _medicalRecordService.Update(medicalRecord);
        }

        public MedicalRecord GetMedicalRecordByPatient(Patient patient)
        {
            return _medicalRecordService.GetByPatient(patient);
        }

        public void AddNewAllergiesToMedicalRecord(Allergies allergies, Patient patient)
        {
            _medicalRecordService.AddNewAllergiesToMedicalRecord(allergies, patient);
        }
        
        public void RemoveAllergiesFromMedicalRecord(Allergies allergies, Patient patient)
        {
            _medicalRecordService.RemoveAllergiesFromMedicalRecord(allergies, patient);

        }
    }
}
