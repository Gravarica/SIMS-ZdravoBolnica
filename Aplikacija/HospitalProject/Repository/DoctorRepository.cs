// File:    DoctorRepository.cs
// Author:  aleksa
// Created: Monday, March 28, 2022 15:56:14
// Purpose: Definition of Class DoctorRepository

using HospitalProject.Exception;
using HospitalProject.FileHandler;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository
{
   public class DoctorRepository
   {

       private IHandleData<Doctor> _doctorFileHandler;

        private List<Doctor> _doctors = new List<Doctor>();

        public DoctorRepository()
        {
            _doctorFileHandler = new DoctorFileHandler(FilePathStorage.DOCTOR_FILE);
            _doctors = _doctorFileHandler.ReadAll().ToList();
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctors;
        }

        public Doctor GetById(int id)
        {
            return GetAll().SingleOrDefault(doctor => doctor.Id == id);
        }

        public Doctor GetLoggedDoctor(string username)
        {
            return GetAll().SingleOrDefault(doctor => doctor.Username.Equals(username));
        }

        public List<Doctor> GetDoctorsBySpecialization(Specialization specialization)
        {
            return _doctors.Where(doctor => doctor.Specialization == specialization).ToList();
        }

        public void UpdateFreeDays(Doctor doctor, int days)
        {
            Doctor updateDoctor = GetById(doctor.Id);
            updateDoctor.FreeDays -= days;
        }

        
    }
}
