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
        private const string NOT_FOUND_ERROR = "Account with {0}:{1} can not be found!";

        private DoctorFileHandler _doctorFileHandler;

        private List<Doctor> _doctors = new List<Doctor>();

        public DoctorRepository(DoctorFileHandler doctorFileHandler)
        {
            _doctorFileHandler = doctorFileHandler;
            _doctors = _doctorFileHandler.ReadAll().ToList();
        }

        public List<Doctor> Doctors 
        { 
            get 
            {
                return _doctors; 
            } 
        }

        // Vraca listu svih entiteta 
        public IEnumerable<Doctor> GetAll()
        {
            return _doctors;
        }


        // Pokupi samo jedan entitet po njegovom ID-u
        public Doctor Get(int id)
        {
            try
            {
                {                                                                           // Vraca ili onaj entitet koji je jedinstven, ili vraca default vrednost
                    return GetAll().SingleOrDefault(account => account.Id == id);           // Daj mi onaj account za koji je account.Id == id
                }
            }
            catch (ArgumentException)
            {
                {
                    throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
                }
            }
        }

        
    }
}
