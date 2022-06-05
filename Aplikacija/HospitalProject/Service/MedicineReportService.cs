using HospitalProject.Model;
using HospitalProject.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace HospitalProject.Service
{
    public class MedicineReportService
    {

        private MedicineReportRepository medicineReportRepository;

        public MedicineReportService(MedicineReportRepository medicineReportRepository)
        {
            this.medicineReportRepository=medicineReportRepository;
        }

        public bool Create(Equipement medicine, string description, Doctor doctor)
        {
            if(CheckIfMedicineReportAlreadyExists(medicine))
            {
                return false;
            }

            medicineReportRepository.Insert(new MedicineReport(medicine, description, doctor));
            return true;
        }

        private bool CheckIfMedicineReportAlreadyExists(Equipement medicine)
        {
            return medicineReportRepository.GetMedicineReportByMedicine(medicine) != null;
        }

        public List<MedicineReport> getAll()
        {
            return medicineReportRepository.GetAll();
        }

        public void Delete(MedicineReport medicineReport)
        {
            medicineReportRepository.Delete(medicineReport);
        }
    }
}
