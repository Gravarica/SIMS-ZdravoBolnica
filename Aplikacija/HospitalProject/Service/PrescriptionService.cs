using HospitalProject.Model;
using HospitalProject.Repository;
using HospitalProject.ValidationRules.DoctorValidation;
using Model;
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
        EquipementService equipementService;
        MedicalRecordService medicalRecordService;

        public PrescriptionService(PrescriptionRepository prescriptionRepository, AppointmentService appointmentService, EquipementService equipementService, MedicalRecordService medicalRecordService)
        {
            this.prescriptionRepository=prescriptionRepository;
            this.appointmentService=appointmentService;
            this.equipementService=equipementService;
            this.medicalRecordService=medicalRecordService;
        }

        public List<Prescription> GetAll()
        {
            var prescriptions = prescriptionRepository.GetAll().ToList();
            BindAppointmentsWithPrescriptions(prescriptions);
            BindMedicinesWithPrescriptions(prescriptions);  
            return prescriptions;
        }

        public List<Prescription> GetPrescriptionsForPatient(int patientId)
        {
            var prescriptions = prescriptionRepository.GetPrescriptionsForPatient(patientId).ToList();
            BindAppointmentsWithPrescriptions(prescriptions);
            BindMedicinesWithPrescriptions(prescriptions);
            return prescriptions;
        }

        private void BindAppointmentsWithPrescriptions(List<Prescription> prescriptions)
        {
            prescriptions.ForEach(SetAppointmentForPrescription);
        }

        private void SetAppointmentForPrescription(Prescription prescription)
        {
            prescription.Appointment = appointmentService.GetById(prescription.Appointment.Id);
        }

        public string Create(Appointment appointment, DateTime startDate, DateTime endDate, int interval, string description, Equipement medicine)
        {
            Allergies returnAllergen = AllergensValidation.CheckIfPatientIsAllergicToMedicine(medicalRecordService.GetById(appointment.Patient.MedicalRecordId).Allergies, medicine);
            if (returnAllergen != null) return returnAllergen.Name;

            Prescription prescription = new Prescription(appointment, startDate, endDate, interval, description, medicine);
            prescriptionRepository.Insert(prescription);

            return null;

        }

        public void Delete(int prescriptionId)
        {
            prescriptionRepository.Delete(prescriptionId);
        }


        public Prescription GetById(int id) {

            var prescription = prescriptionRepository.GetById(id);
            SetAppointmentForPrescription(prescription);
            return prescription;
        }
      
        private void BindMedicinesWithPrescriptions(List<Prescription> prescriptions)
        {
            prescriptions.ForEach(SetMedicineForPrescription);
        }

        private void SetMedicineForPrescription(Prescription prescription)
        {
            prescription.Medicine = equipementService.GetById(prescription.Medicine.Id);
        }
    }
}
