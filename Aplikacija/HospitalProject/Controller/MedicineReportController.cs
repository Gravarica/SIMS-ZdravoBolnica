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
    public class MedicineReportController
    {
        private MedicineReportService medicineReportService;

        public MedicineReportController(MedicineReportService medicineReportService)
        {
            this.medicineReportService=medicineReportService;
        }

        public bool Create(Equipement medicine, string description, Doctor doctor)
        {
            return medicineReportService.Create(medicine, description,doctor);
        }

        public List<MedicineReport> GetAll()
        {
            return medicineReportService.getAll();
        }

        public void Delete(MedicineReport medicineReport)
        {
            medicineReportService.Delete(medicineReport);
        }
    }
}
