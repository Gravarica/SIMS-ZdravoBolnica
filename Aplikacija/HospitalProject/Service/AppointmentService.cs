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
            _roomService = roomService;
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
            appointments.ToList().ForEach(appointment => SetPatient(appointment));
        }

        // For each appointment, sets its doctor field to a certain doctor object by his id written in the file
        private void BindAppointmentsWithDoctors(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach(appointment => SetDoctor(appointment));
        }

        private void BindAppointmentsWithRooms(IEnumerable<Appointment> appointments)
        {
            appointments.ToList().ForEach(appointment => SetRoom(appointment));
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

        public bool RoomHasApointmentByDay(DateOnly startDate, DateOnly endDate, Room room)
        {
            var allApointments = getAll();
            foreach (Appointment ap in allApointments)
            {
                DateOnly date = new DateOnly(ap.Date.Year, ap.Date.Month, ap.Date.Day);
                if (date >= startDate && date <= endDate && ap.Room.Id == room.Id)
                {
                    return false;
                }
            }

            return true;
        }

        // Method that generates available appointments, this method is called in controller
        public List<Appointment> GenerateAvailableAppointments(DateOnly StartDate, DateOnly EndDate, Doctor doctor, Patient patient, ExaminationType examType, Room room)
        {
            List<Appointment> allAppointments = GenerateAllApointments(StartDate, EndDate, doctor, patient, examType, room);
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

            foreach (Appointment appointment in removeAppointments)
            {
                allAppointments.Remove(appointment);
            }

            return allAppointments;
        }

        // Method that generates empty appointments
        private List<Appointment> GenerateAllApointments(DateOnly StartDate, DateOnly EndDate, Doctor doctor, Patient patient, ExaminationType examType, Room room)
        {
            List<Appointment> retAppointments = new List<Appointment>();
            TimeOnly shiftIterator;
            while (StartDate <= EndDate)
            {
                shiftIterator = doctor.ShiftStart;

                while (shiftIterator <= doctor.ShiftEnd)
                {
                    Appointment appointment = new Appointment(StartDate.ToDateTime(shiftIterator), 30, doctor, patient, room, examType);
                    retAppointments.Add(appointment);
                    shiftIterator = shiftIterator.AddMinutes(30);
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

        public void SetAppointmentFinished(Appointment appointment)
        {
            appointmentRepository.SetAppointmentFinished(appointment);
        }

        public IEnumerable<Appointment> GetAllUnifinishedAppointmentsForDoctor(int doctorId)
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


        public IEnumerable<Appointment> GenerateAppointmentsPriorityDoctor(DateOnly startDate, DateOnly endDate, Doctor doctor, Patient patient)
        {


            DateTime date6 = DateTime.Now;
            TimeOnly time = new TimeOnly(date6.Hour, date6.Minute);
            DateTime date1 = startDate.ToDateTime(time);
            DateTime date2 = endDate.ToDateTime(time);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);




            if ((date1 - date6).TotalDays < 5)
            {

                DateOnly date3 = endDate;

                return GenerateAvailableAppointments(today, date3.AddDays(5), doctor, patient, ExaminationType.GENERAL, FindRoomById(3));


            }
            else

            {
                DateOnly date4 = startDate;
                DateOnly date5 = endDate;

                return GenerateAvailableAppointments(date4.AddDays(-5), date5.AddDays(5), doctor, patient, ExaminationType.GENERAL, FindRoomById(3));
            }




        }

        public IEnumerable<Appointment> GenerateAppointmentsPriorityDate(DateOnly startDate, DateOnly endDate, Patient patient)
        {
            IEnumerable<Doctor> allDoctors = _doctorService.getAll();
            List<Appointment> existingAppointments = appointmentRepository.GetAll().ToList();
            List<Appointment> generatedAppointments = new List<Appointment>();

            foreach (Doctor d in allDoctors)
            {


                generatedAppointments.AddRange(GenerateAvailableAppointments(startDate, endDate, d, patient, ExaminationType.GENERAL, FindRoomById(3)));

            }

            //List<Appointment> list3 = generatedAppointments.(existingAppointments).ToList();


            return generatedAppointments;

        }
        //Svi rezervisani termini po specijalizaciji
        public IEnumerable<Appointment> GetBySpecialization(Specialization specialization)
        {
            var appointments = appointmentRepository.GetBySpecialization(specialization);
            // BindDataForAppointments(appointments);
            return appointments;
        }

        //Svi rezervisani termini po specijalizaciji u narednih sat vremena
        public List<Appointment> GetByDoctorInNextHour(Specialization specialization)
        {
            IEnumerable<Doctor> allDoctors = _doctorService.getAll();
            List<Appointment> forEachDoctorFirst = new List<Appointment>(); ; 
            foreach (Doctor d in allDoctors)
            {
                if (d.Specialization == specialization)
                {
                
                 Appointment appointment = appointmentRepository.GetAllUnfinishedAppointmentsForDoctor(d.Id).First();

                 forEachDoctorFirst.Add(appointment);

                 }


            }

            return forEachDoctorFirst;
        }


        public TimeSpan CountTimeBetweenAppointments(Appointment oldAppointment, Appointment newAppointment)
        {
            DateTime oldDate = oldAppointment.Date;
            DateTime newDate = newAppointment.Date;
            TimeOnly oldTime = new TimeOnly(oldDate.Hour, oldDate.Minute);
            TimeOnly newTime = new TimeOnly(newDate.Hour, newDate.Minute);

            TimeSpan difference = newTime - oldTime;


            return difference;
        }

        //Najoptimalniji po specijalizaciji u narednih sat vremena
        private List<AppointmentsDTO> GetOptimalForReschedulingInNextHour(Specialization specialization)
        {

            List<Appointment> firstAppointmentsForEachDoctor = (List<Appointment>)GetByDoctorInNextHour(specialization);

            List<AppointmentsDTO> potentialCombinations = GetOptimalForEachAppointment(firstAppointmentsForEachDoctor);

           
            return potentialCombinations;
        }


        private List<AppointmentsDTO> GetOptimalForEachAppointment(List<Appointment> appointments)
        {

                DateOnly StartDate;
                DateOnly EndDate;
                ExaminationType examType;
                Room room;
                List<AppointmentsDTO> potentialCombinations = new List<AppointmentsDTO>();

            foreach (Appointment appointment in appointments)
            {
                

                List<Appointment> nextAppointments = GenerateAvailableAppointments(StartDate, EndDate, appointment.Doctor, appointment.Patient, appointment.ExaminationType, appointment.Room);

                Appointment bestPotentialAppointment = nextAppointments.First();
                TimeSpan bestTime = CountTimeBetweenAppointments(appointment, bestPotentialAppointment);

                AppointmentsDTO NewAppointmentDTO = new AppointmentsDTO(appointment, appointment.Doctor, bestPotentialAppointment, bestTime);
                potentialCombinations.Add(NewAppointmentDTO);
            }

            return potentialCombinations;
        }


        //PRVI OD SVIH SLOBODNIH kod svih specijalista u narednih dva sata SE UZIMA 
       public Appointment GenerateAvailableAppointmentsBySpecialization(Specialization specialization, Patient patient, ExaminationType examType, Room room)
        {

            DateTime dateNow = DateTime.Now;


            IEnumerable<Doctor> allDoctors = _doctorService.getAll();
            List<Appointment> generatedAppointments = new List<Appointment>();
            foreach (Doctor doctor in allDoctors)
            {
                if (doctor.Specialization == specialization)

                {
                    generatedAppointments.AddRange(GenerateAllApointmentsInNextHour(dateNow, doctor, patient, examType, room));

                }
            }

            return generatedAppointments.First();
        }

        private List<Appointment> GenerateAllApointmentsInNextHour(DateTime StartDate, Doctor doctor, Patient patient, ExaminationType examType, Room room)
        {
            List<Appointment> retAppointments = new List<Appointment>();

            int i;
            for (i = 0; i < 4; i++)

            {
                Appointment appointment = new Appointment(StartDate, 30, doctor, patient, room, examType); //?
                retAppointments.Add(appointment);
                StartDate = StartDate.AddMinutes(30);
            }


            return retAppointments;
        }
      

        public Appointment FirstAvailableWithoutRescheduling(Specialization specialization, Patient patient, ExaminationType examType, Room room)
        {
            Appointment appointment = GenerateAvailableAppointmentsBySpecialization(specialization, patient, examType, room);
            Create(appointment);
            return appointment; //da mi ispise na frontu kod kog doktora i u kojoj je sobi
        }

        public List <AppointmentsDTO> FirstFromEachDoctorWithRescheduling(Specialization specialization)
        {
            List<AppointmentsDTO> eachDoctorsBest = GetOptimalForReschedulingInNextHour(specialization);
           return eachDoctorsBest;
        }

     
    }
}