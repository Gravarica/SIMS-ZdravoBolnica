// File:    AppointmentService.cs
// Author:  aleksa
// Created: Monday, March 28, 2022 15:33:34
// Purpose: Definition of Class AppointmentService

using System;
using Repository;
using Model;
using HospitalProject.Service;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class AppointmentService
    {
        private AppointmentRepository appointmentRepository;
        private PatientService _patientService;
        private DoctorService _doctorService;
        
        public AppointmentService(AppointmentRepository appointmentRepository, PatientService patientService, DoctorService doctorService)
          {
              this.appointmentRepository = appointmentRepository;
              _patientService = patientService;
              _doctorService = doctorService;
          }

        public Appointment Create(Appointment appointment)
        {
           return appointmentRepository.Insert(appointment);
        }
        
        public IEnumerable<Appointment> getAll()
        {
           var patients = _patientService.GetAll();
           var doctors = _doctorService.getAll();
           var appointments = appointmentRepository.GetAll();
           BindAppointmentsWithDoctors(doctors, appointments); 
           BindAppointmentsWithPatients(patients, appointments);
           return appointments;
        }
        
        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }
        
        public void Delete(int id)
        {
           appointmentRepository.Delete(id);
        }
       
        public void BindAppointmentsWithPatients(IEnumerable<Patient> patients, IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach(appointment => appointment.Patient = FindPatientById(patients, appointment.PatientId));
        }

        public void BindAppointmentsWithDoctors(IEnumerable<Doctor> doctors, IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach (appointment => appointment.Doctor = FindDoctorById(doctors, appointment.DoctorId));
        }
      
        public Patient FindPatientById(IEnumerable<Patient> patients, int patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }

        public Doctor FindDoctorById(IEnumerable<Doctor> doctors, int doctorId)
        {
            return doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }
   
   }
}