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
using System.Windows.Automation;

namespace Repository
{

    public class AppointmentRepository
    {
        private IHandleData<Appointment> _appointmentFileHandler;

        private int _appointmentMaxId;

        private List<Appointment> _appointments = new List<Appointment>();

        public AppointmentRepository()
        {
            _appointmentFileHandler = new AppointmentFileHandler(FilePathStorage.APPOINTMENT_FILE);
            _appointments = _appointmentFileHandler.ReadAll().ToList();
            _appointmentMaxId = GetMaxId();
        }

        public List<Appointment> Appointments { get; set; }

        private int GetMaxId() {
            return _appointments.Count() == 0 ? 0 : _appointments.Max(appointment => appointment.Id);
        }

        public Appointment Insert(Appointment appointment)
        {
                appointment.Id = ++_appointmentMaxId;
                _appointmentFileHandler.SaveOneEntity(appointment);
                _appointments.Add(appointment);
                return appointment;
        }

        public Appointment GetById(int id)
        {
            return _appointments.FirstOrDefault(x => x.Id == id); 
        }

        public IEnumerable<Appointment> GetAllUnfinishedAppointments()
        {
                return _appointments.Where(x=> x.IsDone==false);  
        }

        public IEnumerable<Appointment> GetAllByRoomId(int id)
        {
            return _appointments.Where(x=> x.Room.Id==id && x.IsDone==false);  
        }
        
        public IEnumerable<Appointment> GetAll()
        {
            return _appointments;
        }

        public void Delete(int id)
        {
            Appointment removeAppointment = GetById(id);
            _appointments.Remove(removeAppointment);
            _appointmentFileHandler.Save(_appointments);
        }
        
        public bool DeleteApointmentsByRoomId(int id)
        {
            var apointments = GetAllByRoomId(id);
            return apointments.Count() == 0;
        }

        public void Update(Appointment appointment)
        {
                Appointment updatedAppointment = GetById(appointment.Id);

                updatedAppointment.Date = appointment.Date;
                updatedAppointment.Duration = appointment.Duration;
                updatedAppointment.Patient.Id = appointment.Patient.Id;
                updatedAppointment.Room.Id = appointment.Room.Id;

                _appointmentFileHandler.Save(_appointments);
        }

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

        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForDoctor(int doctorId)
        {
            return _appointments.Where(appointment => appointment.IsDone == false && appointment.Doctor.Id == doctorId);
        }

        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForPatient(int patientId)
        {
            return _appointments.Where(x => x.Patient.Id == patientId && x.IsDone == false);
        }

        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForRoom(int roomId)
        {
            return _appointments.Where((x) => x.Room.Id == roomId && x.IsDone == false);
        }

    }

}