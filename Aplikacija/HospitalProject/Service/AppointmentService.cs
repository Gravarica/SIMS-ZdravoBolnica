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
using HospitalProject.Model;

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

        public bool DeleteApointmentsByRoomId(int id)
        {
           return appointmentRepository.DeleteApointmentsByRoomId(id);
        }
        
        // Updates given appointment to certain parameters
        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }

        public Appointment GetById(int id)
        {
            var appointment = appointmentRepository.GetById(id);
            BindDataForOneAppointment(appointment);
            return appointment;
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

        private void BindDataForOneAppointment(Appointment appointment)
        {
            SetPatient(appointment);
            SetDoctor(appointment);
            SetRoom(appointment);  
        }
       
        // For each appointment, sets its patient field to a certain patient by his id given in the file
        private void BindAppointmentsWithPatients(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach(SetPatient);
        }

        // For each appointment, sets its doctor field to a certain doctor object by his id written in the file
        private void BindAppointmentsWithDoctors(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach (SetDoctor);
        }

        private void BindAppointmentsWithRooms(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach(SetRoom);
        }

        private void SetPatient(Appointment appointment)
        {
            appointment.Patient = FindPatientById(appointment.Patient.Id);
        }

        private void SetDoctor(Appointment appointment)
        {
            appointment.Doctor = FindDoctorById(appointment.Doctor.Id);
        }

        private void SetRoom(Appointment appointment)
        {
            appointment.Room = FindRoomById(appointment.Room.Id);
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
            List<Appointment> retAppointmentsDoctor = appointmentRepository.GetAppointmentsForDoctor(doctor.Id).ToList();
            List<Appointment> retAppointmentsPatient = appointmentRepository.GetAppointmentsForPatient(patient.Id).ToList();
            List<Appointment> unionAppointments = retAppointmentsDoctor.Union(retAppointmentsPatient).ToList();
            BindDataForAppointments(unionAppointments);
            return unionAppointments;
        }

        public bool RoomHasAppointmentByDay(DateOnly startDate, DateOnly endDate, Room room)
        {
            var allApointments = getAll();
            foreach (Appointment ap in allApointments)
            {
                DateOnly date = new DateOnly(ap.Date.Year,ap.Date.Month,ap.Date.Day);
                if (date >= startDate && date <= endDate && ap.Room.Id == room.Id)
                {
                    return false;
                }
            }

            return true;
        }

        // Method that generates available appointments, this method is called in controller
        public List<Appointment> GenerateAvailableAppointments(DateOnly StartDate, DateOnly EndDate, Doctor doctor, Patient patient, ExaminationType examType, Room room, int priorityFlag, bool stopRecursion)
        { 
            List<Appointment> generatedAppointments = GenerateAllAppointments(StartDate, EndDate, doctor, patient, examType, room);
            List<Appointment> scheduledAppointments = GetAppointmentsByDoctorAndPatient(doctor, patient);
            List<Appointment> filteredList = RemoveScheduledAppointments(generatedAppointments, scheduledAppointments);

            if (GeneratedAppointmentListIsNotEmpty(filteredList)) return filteredList;

            return GenerateAppointmentsByPriority(StartDate, EndDate, doctor, patient, priorityFlag, stopRecursion);

        }

        private List<Appointment> GenerateAppointmentsByPriority(DateOnly StartDate, DateOnly EndDate, Doctor doctor,
            Patient patient, int priorityFlag, bool StopRecursion)
        {
            if (StopRecursion) return new List<Appointment>();
            
            if (priorityFlag == 1) return GenerateAppointmentsPriorityDoctor(StartDate, EndDate, doctor, patient).ToList();

            if (priorityFlag == 2) return GenerateAppointmentsPriorityDate(StartDate, EndDate, patient, doctor).ToList();

            return new List<Appointment>(); 
        }

        private bool GeneratedAppointmentListIsNotEmpty(List<Appointment> appointments)
        {
            return appointments.Any();
        }

        // Method that removes already scheduled appointments
        private List<Appointment> RemoveScheduledAppointments(List<Appointment> generatedAppointmentsCollection, List<Appointment> scheduledAppointments)
        {
            GetAllAppointmentsThatShouldBeRemoved(generatedAppointmentsCollection, scheduledAppointments).ForEach(appointment => RemoveFromCollection(generatedAppointmentsCollection, appointment));

            return generatedAppointmentsCollection;
        }

        private List<Appointment> GetAllAppointmentsThatShouldBeRemoved(List<Appointment> generatedAppointmentsCollection, List<Appointment> scheduledAppointments)
        {
            List<Appointment> appointmentsToRemove = new List<Appointment>();
            generatedAppointmentsCollection.ForEach(generatedAppointment => appointmentsToRemove.AddRange(SelectExistingAppointmentsToRemove(scheduledAppointments, generatedAppointment)));
            return appointmentsToRemove;
        }

        private IEnumerable<Appointment> SelectExistingAppointmentsToRemove(List<Appointment> scheduledAppointments, Appointment generatedAppointment)
        {
            return from scheduledAppointment in scheduledAppointments where generatedAppointment.Equals(scheduledAppointment) select generatedAppointment;
        }

        private void RemoveFromCollection(List<Appointment> appointments, Appointment removeAppointment)
        {
            appointments.Remove(removeAppointment);
        }

        // Method that generates empty appointments
        private List<Appointment> GenerateAllAppointments(DateOnly StartDate, DateOnly EndDate, Doctor doctor, Patient patient, ExaminationType examType, Room room)
        {
            List<Appointment> generatedAppointments = new List<Appointment>();
            while (StartDate <= EndDate)
            {
                GenerateAppointmentsInOneDay(StartDate, doctor, patient, examType, room, generatedAppointments);
                StartDate = StartDate.AddDays(1);
            }

            return generatedAppointments;
        }

        private static void GenerateAppointmentsInOneDay(DateOnly StartDate, Doctor doctor, Patient patient,
            ExaminationType examType, Room room, List<Appointment> retAppointments)
        {
            var shiftIterator = doctor.ShiftStart;

            while (shiftIterator <= doctor.ShiftEnd)
            {
                Appointment appointment = new Appointment(StartDate.ToDateTime(shiftIterator), 30, doctor, patient, room, examType);
                retAppointments.Add(appointment);
                shiftIterator = shiftIterator.AddMinutes(30);
            }
        }

        public IEnumerable<Appointment> GetAllUnfinishedAppointments()
        { 
            var appointments = appointmentRepository.GetAllUnfinishedAppointments();
            BindDataForAppointments(appointments);
            return appointments;
        }

        public void SetAppointmentFinished(Appointment appointment)
        {
            appointmentRepository.SetAppointmentFinished(appointment);
        }

        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForDoctor(int doctorId)
        {
            var appointments = appointmentRepository.GetAllUnfinishedAppointmentsForDoctor(doctorId);
            BindDataForAppointments(appointments);
            return appointments;
        }

        public IEnumerable<Appointment> GetAllUnfinishedAppointmentsForPatient(int patientId)
        {
            var appointments = appointmentRepository.GetAllUnfinishedAppointmentsForPatient(patientId);
            BindDataForAppointments(appointments);
            return appointments;
        }

        public IEnumerable<Appointment> GenerateAppointmentsPriorityDoctor(DateOnly startDate, DateOnly endDate , Doctor doctor, Patient patient)
        {
            
            TimeOnly time = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute) ;
            DateTime startDateTime = startDate.ToDateTime(time);
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            if ((startDateTime - DateTime.Now).TotalDays < 5) return GenerateAvailableAppointments(today, endDate.AddDays(5), doctor, patient, ExaminationType.GENERAL, FindRoomById(3), 1, true);

            return GenerateAvailableAppointments(startDate.AddDays(-5), endDate.AddDays(5), doctor, patient, ExaminationType.GENERAL, FindRoomById(3), 1, true);

        }

        public IEnumerable<Appointment> GenerateAppointmentsPriorityDate(DateOnly startDate, DateOnly endDate, Patient patient, Doctor doctor)
        {
            List<Appointment> generatedAppointments = new List<Appointment>();

            foreach (Doctor iteratorDoctor in _doctorService.GetDoctorsBySpecialization(doctor.Specialization)) {
                generatedAppointments.AddRange(GenerateAvailableAppointments(startDate, endDate, iteratorDoctor, patient, ExaminationType.GENERAL, doctor.Ordination, 2, true));
            }

            return generatedAppointments;

        }

        private List<Appointment> GetAppointmentsForDoctor(Doctor doctor)
        {
            var list = appointmentRepository.GetAppointmentsForDoctor(doctor.Id).ToList();
            BindDataForAppointments(list);
            return list;
        }

        public List<Patient> GetAllPatientsThatHadAppointmentWithDoctor(Doctor doctor)
        {
            return GetAppointmentsForDoctor(doctor).Select(appointment => appointment.Patient).Distinct().ToList();
        }
    }
}