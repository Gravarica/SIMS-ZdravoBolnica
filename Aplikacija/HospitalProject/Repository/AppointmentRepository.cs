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
            _appointmentMaxId = GetMaxId(_appointments);
        }

        public List<Appointment> Appointments { get; set; }

        // Method that calculates what is max id of an appointment in the system 
        private int GetMaxId(IEnumerable<Appointment> appointments) {
            return appointments.Count() == 0 ? 0 : appointments.Max(appointment => appointment.Id);
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
                updatedAppointment.PatientId = appointment.PatientId;
                updatedAppointment.RoomID = appointment.RoomID;

                _appointmentFileHandler.Save(_appointments);
        }

}

}