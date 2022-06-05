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
    public class PrescriptionController
    {

        PrescriptionService prescriptionService;

        public PrescriptionController(PrescriptionService prescriptionService)
        {
            this.prescriptionService=prescriptionService;
        }

        public List<Prescription> GetAll()
        {
            return prescriptionService.GetAll();
        }

        public List<Prescription> GetPrescriptionsForPatient(int patientId)
        {
            return prescriptionService.GetPrescriptionsForPatient(patientId);
        }

        public string Create(Appointment appointment, DateTime startDate, DateTime endDate, int interval, string description, Equipement medicine)
        {
            return prescriptionService.Create(appointment, startDate, endDate, interval, description, medicine);
        }

        public void Delete(int prescriptionId)
        {
            prescriptionService.Delete(prescriptionId);
        }
    }
}
