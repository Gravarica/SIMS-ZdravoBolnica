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
using HospitalProject.ValidationRules.DoctorValidation;

namespace Service
{
    public class AppointmentService
    {
        private AppointmentRepository appointmentRepository;
        private PatientService _patientService;
        private DoctorService _doctorService;
        private RoomService _roomService;

        public AppointmentService(AppointmentRepository appointmentRepository, PatientService patientService, DoctorService doctorService, RoomService roomService)
        {
            this.appointmentRepository = appointmentRepository;
            _patientService = patientService;
            _doctorService = doctorService;
            _roomService=roomService;
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
            BindAppointmentsWithRooms(appointments);
        }
       
        // For each appointment, sets its patient field to a certain patient by his id given in the file
        private void BindAppointmentsWithPatients(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach(appointment => appointment.Patient = FindPatientById(appointment.Patient.Id));
        }

        // For each appointment, sets its doctor field to a certain doctor object by his id written in the file
        private void BindAppointmentsWithDoctors(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach (appointment => appointment.Doctor = FindDoctorById(appointment.Doctor.Id));
        }

        private void BindAppointmentsWithRooms(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach(appointment => appointment.Room = FindRoomById(appointment.Room.Id));
        }
      
        private Patient FindPatientById(int patientId)
        {
            return _patientService.GetById(patientId);
        }

        private Doctor FindDoctorById(int doctorId)
        {
            return _doctorService.GetById(doctorId);
        }

        private Room FindRoomById(int roomId)
        {
            return _roomService.Get(roomId);
        }

        // Method that gets all reserved appointments in the system 
        private List<Appointment> GetAppointmentsByDoctorAndPatient(Doctor doctor, Patient patient)
        {
            List<Appointment> retAppointmentsDoctor = new List<Appointment>();
            List<Appointment> retAppointmentsPatient = new List<Appointment>();
            var appointments = appointmentRepository.GetAll();
            BindDataForAppointments(appointments);

            foreach (Appointment appointment in appointments)
            {
                if (doctor.Id == appointment.Doctor.Id)
                {
                    retAppointmentsDoctor.Add(appointment);
                }
                if (patient.Id == appointment.Patient.Id)
                {
                    retAppointmentsPatient.Add(appointment);
                }
            }

            return retAppointmentsDoctor.Union(retAppointmentsPatient).ToList();
        }

        // Method that generates available appointments, this method is called in controller
        public List<Appointment> GenerateAvailableAppointments(DateOnly StartDate, DateOnly EndDate, Doctor doctor, Patient patient)
        {

            List<Appointment> allAppointments = GenerateAllApointments(StartDate, EndDate, doctor, patient);
            var existingAppointments = GetAppointmentsByDoctorAndPatient(doctor, patient);

            return MinusAppointments(allAppointments, existingAppointments);
        }

        // Method that removes already scheduled appointments
        private List<Appointment> MinusAppointments(List<Appointment> allAppointments, List<Appointment> existingAppointments)
        {

            List<Appointment> removeAppointments = new List<Appointment>();

            foreach (Appointment generatedAppointment in allAppointments)
            {
                foreach (Appointment existingAppointment in existingAppointments)
                {
                    if (generatedAppointment.Equals(existingAppointment))
                    {
                        removeAppointments.Add(generatedAppointment);
                    }
                }
            }

            foreach(Appointment appointment in removeAppointments)
            {
                allAppointments.Remove(appointment);
            }

            return allAppointments;
        }

        // Method that generates empty appointments
        private List<Appointment> GenerateAllApointments(DateOnly StartDate, DateOnly EndDate, Doctor doctor, Patient patient)
        {
            List<Appointment> retAppointments = new List<Appointment>();
            TimeOnly shiftStartConst = new TimeOnly(8, 0);
            TimeOnly shiftStart;
            TimeOnly shiftEnd = new TimeOnly(15, 0);
            while(StartDate <= EndDate)
            {
                shiftStart = shiftStartConst;

                while(shiftStart <= shiftEnd)
                { 
                    Appointment appointment = new Appointment(StartDate.ToDateTime(shiftStart), 30, doctor, patient, FindRoomById(2), HospitalProject.Model.ExaminationType.GENERAL);
                    retAppointments.Add(appointment);
                    shiftStart = shiftStart.AddMinutes(30);
                }
                StartDate = StartDate.AddDays(1);
            }

            return retAppointments;
        }


        public IEnumerable<Appointment> GetAllUnfinishedAppointments()
        {

            var appointments = appointmentRepository.GetAllUnfinishedAppointments();
            BindDataForAppointments(appointments);
            return appointments;
        }

    }
}