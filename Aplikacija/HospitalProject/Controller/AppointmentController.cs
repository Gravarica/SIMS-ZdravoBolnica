// File:    AppointmentController.cs
// Author:  aleksa
// Created: Monday, March 28, 2022 16:14:26
// Purpose: Definition of Class AppointmentController

using System;
using Service;
using Model;
using System.Collections.Generic;
using System.Reflection;
using HospitalProject.Model;

namespace Controller
{
   public class AppointmentController
   {
      private AppointmentService _appointmentService;

      public AppointmentController(AppointmentService appointmentService)
      {
         _appointmentService = appointmentService;
      }
      
      public Appointment Create(Appointment appointment)
      {
        return  _appointmentService.Create(appointment);
      }

      public bool DeleteApointmentsByRoomId(int id)
      {
          return _appointmentService.DeleteApointmentsByRoomId(id);
      }
      public IEnumerable<Appointment> GetAll()
      {
            return _appointmentService.getAll();
      }

      public IEnumerable<Appointment> GetAllUnfinishedAppointments()
      {
            return _appointmentService.GetAllUnfinishedAppointments();
      }
      
      public void Update(Appointment appointment)
      {
         _appointmentService.Update(appointment);
      }
      
      public void Delete(int id)
      {
            _appointmentService.Delete(id);
      }

      public List<Appointment> GenerateAvailableAppointments(DateOnly startDate, DateOnly endDate, Doctor doctor, Patient patient, ExaminationType examType, Room room, int priorityFlag)
      {
          return _appointmentService.GenerateAvailableAppointments(startDate, endDate, doctor, patient, examType, room, priorityFlag, false);
      }

        public IEnumerable<Appointment> GetAllUnifinishedAppointmentsForDoctor(int doctorId)
        {
            return _appointmentService.GetAllUnfinishedAppointmentsForDoctor(doctorId);
        }

        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForPatient(int patientId)
        {
            return _appointmentService.GetAllUnfinishedAppointmentsForPatient(patientId);
        }


        public IEnumerable<Appointment> GenerateAppointmentsPriorityDoctor(DateOnly startDate, DateOnly endDate,Doctor doctor, Patient patient)
        {
            return _appointmentService.GenerateAppointmentsPriorityDoctor(startDate,endDate,doctor,patient);
        }

        public IEnumerable<Appointment> GenerateAppointmentsPriorityDate(DateOnly startDate, DateOnly endDate, Patient patient, Doctor doctor)
        {
            return _appointmentService.GenerateAppointmentsPriorityDate(startDate, endDate,patient, doctor);
        }
    }
}