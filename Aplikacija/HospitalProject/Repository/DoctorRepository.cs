// File:    DoctorRepository.cs
// Author:  aleksa
// Created: Monday, March 28, 2022 15:56:14
// Purpose: Definition of Class DoctorRepository

using HospitalProject.Exception;
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
        private string _path;       // Putanja do fajla 
        private string _delimiter;  // Delimiter za CSV format 
        private List<Doctor> _doctors = new List<Doctor>();

        public DoctorRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _doctors = (List<Doctor>)GetAll();
        }

        public List<Doctor> Doctors 
            { get { return _doctors; } }

        // Vraca listu svih entiteta 
        public IEnumerable<Doctor> GetAll()
        {
            return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
                .Select(ConvertCSVFormatToAccount)
                .ToList();
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

        private Doctor ConvertCSVFormatToAccount(string acountCSVFormat)                   // Ovo prebacuje iz CSV formata i kreira objekat
        {
            var tokens = acountCSVFormat.Split(_delimiter.ToCharArray());
            return new Doctor(int.Parse(tokens[0]), tokens[1], tokens[2], tokens[3]);
        }
    }
}
