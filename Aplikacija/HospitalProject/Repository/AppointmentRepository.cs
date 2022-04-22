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

        private int GetMaxId(IEnumerable<Appointment> appointments) {
            return appointments.Count() == 0 ? 0 : appointments.Max(appointment => appointment.Id);
        }

        public Appointment Insert(Appointment appointment)
        {
                appointment.Id = ++_appointmentMaxId;
                _appointmentFileHandler.AppendLineToFile(appointment);
                return appointment;
        }

        // Proveriti null pointer ovde ako ne vrati nista!!!
        public Appointment Get(int id)
        {
            return _appointments.FirstOrDefault(x => x.Id == id); 
        }

        public IEnumerable<Appointment> GetAll()
            {
                return _appointments;
            }

        public void Delete(int id)
        {
            Appointment removeAppointment = Get(id);
            _appointments.Remove(removeAppointment);
            _appointmentFileHandler.Save(_appointments);
        }

        public void Update(Appointment appointment)
        {

                Appointment updatedAppointment = new Appointment();

                foreach (Appointment app in _appointments)
                {
                    if (appointment.Id == app.Id) {
                        updatedAppointment = app;
                        break;
                    }
                }

                updatedAppointment.Date = appointment.Date;
                updatedAppointment.Duration = appointment.Duration;
                updatedAppointment.PatientId = appointment.PatientId;
                updatedAppointment.RoomID = appointment.RoomID;

                _appointmentFileHandler.Save(_appointments);

        }

}

}