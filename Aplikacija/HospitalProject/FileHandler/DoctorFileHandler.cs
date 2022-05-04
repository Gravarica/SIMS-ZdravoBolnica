﻿using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class DoctorFileHandler
    {

        private string _path;

        private string _delimiter;

        public DoctorFileHandler(string path, string delimiter)
        {
            _path=path;
            _delimiter=delimiter;
        }

        private Doctor ConvertCSVFormatToDoctor(string CSVFormat)                   
        {
            var tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new Doctor(int.Parse(tokens[0]), tokens[1], tokens[2], tokens[3], TimeOnly.Parse(tokens[4]), TimeOnly.Parse(tokens[5]));
        }

        public IEnumerable<Doctor> ReadAll()
        {
            return File.ReadAllLines(_path)             
                   .Select(ConvertCSVFormatToDoctor)   
                   .ToList();
        }

    }
}
