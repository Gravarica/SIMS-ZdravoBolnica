// File:    AppointmentRepository.cs
// Author:  aleksa
// Created: Monday, March 28, 2022 15:02:45
// Purpose: Definition of Class AppointmentRepository

using HospitalProject.FileHandler;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{

    public class AppointmentRepository
    {
        private AppointmentFileHandler _appointmentFileHandler;

        private int _appointmentMaxId;

        private List<Appointment> _appointments = new List<Appointment>();

        public AppointmentRepository(AppointmentFileHandler appointmentFileHandler)
        {
            _appointmentFileHandler=appointmentFileHandler;
            _appointments = _appointmentFileHandler.ReadAll().ToList();
            _appointmentMaxId = GetMaxId();
        }

        public List<Appointment> Appointments { get; set; }

        // Method that calculates what is max id of an appointment in the system 
        private int GetMaxId() {
            return _appointments.Count() == 0 ? 0 : _appointments.Max(appointment => appointment.Id);
        }

        // Method that inserts new appointment in the system
        public Appointment Insert(Appointment appointment)
        {
                appointment.Id = ++_appointmentMaxId;
                _appointmentFileHandler.AppendLineToFile(appointment);
                _appointments.Add(appointment);
                return appointment;
        }

        // Method that returns appointment by his given id 
        public Appointment GetById(int id)
        {
            return _appointments.FirstOrDefault(x => x.Id == id); 
        }

        // Method that returns all appointments in the system
        public IEnumerable<Appointment> GetAllUnfinishedAppointments()
        {
                return _appointments.Where(x=> x.IsDone==false);  
        }

        public IEnumerable<Appointment> GetAllByRoomId(int id)
        {
            return _appointments.Where(x=> x.Room.Id==id);  
        }
        
        public IEnumerable<Appointment> GetAll()
        {
            return _appointments;
        }

        // Method that deletes an appointment by given id
        public void Delete(int id)
        {
            Appointment removeAppointment = GetById(id);
            _appointments.Remove(removeAppointment);
            _appointmentFileHandler.Save(_appointments);
        }

        // Method that updates certain appointment
        public void Update(Appointment appointment)
        {
                Appointment updatedAppointment = GetById(appointment.Id);

                updatedAppointment.Date = appointment.Date;
                updatedAppointment.Duration = appointment.Duration;
                updatedAppointment.Patient.Id = appointment.Patient.Id;
                updatedAppointment.Room.Id = appointment.Room.Id;

                _appointmentFileHandler.Save(_appointments);
        }

        // Method that sets appointment as finished after examination
        public void SetAppointmentFinished(Appointment appointment)
        {
            Appointment updatedAppointment = GetById(appointment.Id);

            updatedAppointment.IsDone = true;

            _appointmentFileHandler.Save(_appointments);
        }

        public IEnumerable<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            return _appointments.Where(appointment => appointment.Doctor.Id == doctorId);
        }

        public IEnumerable<Appointment> GetAppointmentsForPatient(int patientId)
        {
            return _appointments.Where(appointment => appointment.Patient.Id == patientId);
        }

        // Method that returns all appointments for a given doctor
        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForDoctor(int doctorId)
        {
            return _appointments.Where(appointment => appointment.IsDone == false && appointment.Doctor.Id == doctorId);
        }

        // Method that returns all appointments for a given patient
        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForPatient(int patientId)
        {
            return _appointments.Where(x => x.Patient.Id == patientId && x.IsDone == false);
        }

        // Method that returns all appointments for a given room 
        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForRoom(int roomId)
        {
            return _appointments.Where((x) => x.Room.Id == roomId && x.IsDone == false);
        }
}

}