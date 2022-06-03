using HospitalProject.FileHandler;
using HospitalProject.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class MedicineReportRepository
    {
        private MedicineReportFileHandler medicineReportFileHandler;
        private EquipementRepository equipementRepository;
        private DoctorRepository doctorRepository;
        private List<MedicineReport> medicineReports;
        private int medicineReportMaxId;

        public MedicineReportRepository(MedicineReportFileHandler medicineReportFileHandler, EquipementRepository equipementRepository, DoctorRepository doctorRepository)
        {
            this.medicineReportFileHandler=medicineReportFileHandler;
            this.equipementRepository=equipementRepository;
            this.doctorRepository=doctorRepository;
            InstantiateData();
        }

        private void InstantiateData()
        {
            medicineReports = medicineReportFileHandler.ReadAll().ToList();
            BindMedicineReportsForMedicinesAndDoctors();
            medicineReportMaxId = GetMaxId();
        }

        private int GetMaxId()
        {
            return !medicineReports.Any() ? 0 : medicineReports.Max(medicineReport => medicineReport.Id);
        }

        private void BindMedicineReportsForMedicinesAndDoctors()
        {
            medicineReports.ForEach(SetMedicineAndDoctorForReport);    
        }

        private void SetMedicineAndDoctorForReport(MedicineReport medicineReport)
        {
            SetMedicineForReport(medicineReport);
            SetDoctorForReport(medicineReport);
        }

        private void SetMedicineForReport(MedicineReport medicineReport)
        {
            medicineReport.Medicine = equipementRepository.GetById(medicineReport.Medicine.Id);
        }

        private void SetDoctorForReport(MedicineReport medicineReport)
        {
            medicineReport.Doctor = doctorRepository.GetById(medicineReport.Doctor.Id);
        }

        public void Insert(MedicineReport medicineReport)
        {
            medicineReport.Id = ++medicineReportMaxId;
            medicineReports.Add(medicineReport);
            medicineReportFileHandler.AppendLineToFile(medicineReport);
        }

        public MedicineReport GetMedicineReportByMedicine(Equipement medicine)
        {
            return medicineReports.FirstOrDefault(medicineReport => medicineReport.Medicine == medicine);
        }

        public List<MedicineReport> GetAll()
        {
            return medicineReports;
        }

        public void Delete(MedicineReport medicineReport)
        {
            medicineReports.Remove(medicineReport);
            medicineReportFileHandler.Save(medicineReports);
        }

    }
}
