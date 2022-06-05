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
    public class PrescriptionRepository
    {

        private PrescriptionFileHandler prescriptionFileHandler;
        private AppointmentRepository appointmentRepository;
        private int prescriptionMaxId;
        private List<Prescription> prescriptions;

        public PrescriptionRepository(PrescriptionFileHandler prescriptionFileHandler, AppointmentRepository appointmentRepository)
        {
            this.prescriptionFileHandler = prescriptionFileHandler;
            this.appointmentRepository = appointmentRepository;
            InstantiatePrescriptionList();
        }

        private void InstantiatePrescriptionList()
        {
            prescriptions = prescriptionFileHandler.ReadAll().ToList();
            prescriptions.ForEach(SetAppointmentForPrescription);
            prescriptionMaxId = GetMaxId();
        }

        private int GetMaxId()
        {
            return !prescriptions.Any() ? 0 : prescriptions.Max(prescription => prescription.Id);
        }        

        public IEnumerable<Prescription> GetAll()
        {
            return prescriptions;
        }

        public Prescription GetById(int prescriptionId)
        {
            return prescriptions.FirstOrDefault(prescription => prescription.Id == prescriptionId);
        }

        public IEnumerable<Prescription> GetPrescriptionsForPatient(int patientId)
        {
            return prescriptions.Where(prescription => prescription.Appointment.Patient.Id == patientId);
        }

        private void SetAppointmentForPrescription(Prescription prescription)
        {
            prescription.Appointment = appointmentRepository.GetById(prescription.Appointment.Id);
        }

        public void Insert(Prescription prescription)
        {
            prescription.Id = ++prescriptionMaxId;
            prescriptions.Add(prescription);
            prescriptionFileHandler.AppendLineToFile(prescription);
        }

        public void Delete(int prescriptionId)
        {
            Prescription deletePrescription = GetById(prescriptionId);
            prescriptions.Remove(deletePrescription);
            prescriptionFileHandler.Save(prescriptions);
        }

        
    }
}
