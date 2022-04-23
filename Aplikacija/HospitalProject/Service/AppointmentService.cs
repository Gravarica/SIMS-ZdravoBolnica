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

        // Creates a new appointment in the system
        public Appointment Create(Appointment appointment)
        {
           return appointmentRepository.Insert(appointment);
        }
        
        // Returns all of the appointments in the system
        public IEnumerable<Appointment> getAll()
        {
           var appointments = appointmentRepository.GetAll();
           BindDataForAppointments(appointments);
           return appointments;
        }
        
        // Updates given appointment to certain parameters
        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }
        
        // Deletes(Cancels) an appointment in the system by the given id
        public void Delete(int id)
        {
           appointmentRepository.Delete(id);
        }

        // Method that calls all binding methods
        private void BindDataForAppointments(IEnumerable<Appointment> appointments)
        {
            BindAppointmentsWithDoctors(appointments);
            BindAppointmentsWithPatients(appointments);
        }
       
        // For each appointment, sets its patient field to a certain patient by his id given in the file
        private void BindAppointmentsWithPatients(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach(appointment => appointment.Patient = FindPatientById(appointment.PatientId));
        }

        // For each appointment, sets its doctor field to a certain doctor object by his id written in the file
        private void BindAppointmentsWithDoctors(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach (appointment => appointment.Doctor = FindDoctorById(appointment.DoctorId));
        }
      
        private Patient FindPatientById(int patientId)
        {
            return _patientService.GetById(patientId);
        }

        private Doctor FindDoctorById(int doctorId)
        {
            return _doctorService.GetById(doctorId);
        }
   
   }
}