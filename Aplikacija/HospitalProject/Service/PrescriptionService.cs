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
            prescriptions.ForEach(prescription => SetAppointmentForPrescription(prescription));
        }

        private void SetAppointmentForPrescription(Prescription prescription)
        {
            prescription.Appointment = appointmentService.GetById(prescription.Appointment.Id);
        }

        public string Create(Appointment appointment, DateOnly startDate, DateOnly endDate, int interval, string description, Equipement medicine)
        {
            Prescription prescription;
            Allergies returnAllergen = AllergensValidation.CheckIfPatientIsAllergicToMedicine(medicalRecordService.GetById(appointment.Patient.Id).Allergies, medicine);
            if(returnAllergen == null)
            {
                prescription = new Prescription(appointment, startDate, endDate, interval, description, medicine);
                prescriptionRepository.Insert(prescription);
                return null;
            } 
            else
            {
                return returnAllergen.Name;
            }
            //Prescription prescription = new Prescription(appointment, startDate, endDate, interval, description, medicine);
            //prescriptionRepository.Insert(prescription);
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
            prescriptions.ForEach(prescription => SetMedicineForPrescription(prescription));
        }

        private void SetMedicineForPrescription(Prescription prescription)
        {
            prescription.Medicine = equipementService.GetById(prescription.Medicine.Id);
        }
    }
}
