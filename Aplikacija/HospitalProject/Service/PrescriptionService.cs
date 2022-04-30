using HospitalProject.Model;
using HospitalProject.Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class PrescriptionService
    {

        PrescriptionRepository prescriptionRepository;
        AppointmentService appointmentService;
        MedicalRecordService medicalRecordService;

        public PrescriptionService(PrescriptionRepository prescriptionRepository, AppointmentService appointmentService)
        {
            this.prescriptionRepository=prescriptionRepository;
            this.appointmentService=appointmentService;
        }

        public List<Prescription> GetAll()
        {
            var prescriptions = prescriptionRepository.GetAll().ToList();
            BindAppointmentsWithPrescriptions(prescriptions);
            return prescriptions;
        }

        public List<Prescription> GetPrescriptionsForPatient(int patientId)
        {
            var prescriptions = prescriptionRepository.GetPrescriptionsForPatient(patientId).ToList();
            BindAppointmentsWithPrescriptions(prescriptions);
            return prescriptions;
        }

        private void BindAppointmentsWithPrescriptions(List<Prescription> prescriptions)
        {
            prescriptions.ForEach(prescription => SetAppointmentForPrescription(prescription));
        }

        private void SetAppointmentForPrescription(Prescription prescription)
        {
            prescription.Appointment = appointmentService.GetById(prescription.Appointment.Id);
        }

        public void Create(Prescription prescription)
        {
            prescriptionRepository.Insert(prescription);
        }

        public void Delete(int prescriptionId)
        {
            prescriptionRepository.Delete(prescriptionId);
        }
    }
}
