
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HospitalProject.Exception;
using Model;

namespace Repository
{
   public class PatientRepository
   {
        private const string NOT_FOUND_ERROR = "Account with {0}:{1} can not be found!";
        private string _path;       // Putanja do fajla 
        private string _delimiter;  // Delimiter za CSV format
        private int _patientMaxId;
        private List<Patient> _patients = new List<Patient>();

        public PatientRepository(string path, string delimiter)
        {
            _path = path;
            _delimiter = delimiter;
            _patients = (List<Patient>)GetAll();
            _patientMaxId = GetMaxId(GetAll());
        }

        private int GetMaxId(IEnumerable<Patient> patients)
        {
            return patients.Count() == 0 ? 0 : patients.Max(patient => patient.Id);
        }

        public List<Patient> Patients
        { get { return _patients; } }

        // Vraca listu svih entiteta 
        public IEnumerable<Patient> GetAll()
        {
            return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
                .Select(ConvertCSVFormatToAccount)
                .ToList();
        }

        public Patient Add(Patient patient)
        {
            patient.Id = ++_patientMaxId;
            AppendLineToFile(_path, ConvertPatientToCSVFormat(patient));
            return patient;
        }
        public void Delete(string id)
        {
            List<Patient> patients = GetAll().ToList();

            Patient p = patients.SingleOrDefault(r => r.Id.Equals(id));

            if (p != null)
            {
                patients.Remove(p);
                Save();
            }

        }

        public void Save()
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (Patient patient in _patients)
                {
                    file.WriteLine(ConvertPatientToCSVFormat(patient));
                }
            }
        }
        // Pokupi samo jedan entitet po njegovom ID-u
        public Patient Get(int id)
        {
            try
            {
                {                                                                           // Vraca ili onaj entitet koji je jedinstven, ili vraca default vrednost
                    return GetAll().SingleOrDefault(patient => patient.Id == id);           // Daj mi onaj account za koji je account.Id == id
                }
            }
            catch (ArgumentException)
            {
                {
                    throw new NotFoundException(string.Format(NOT_FOUND_ERROR, "id", id));
                }
            }
        }

        private Patient ConvertCSVFormatToAccount(string acountCSVFormat)                   // Ovo prebacuje iz CSV formata i kreira objekat
        {
            var tokens = acountCSVFormat.Split(_delimiter.ToCharArray());
            return new Patient(int.Parse(tokens[0]), tokens[1], tokens[2], tokens[3]);
        }
        private string ConvertPatientToCSVFormat(Patient patient)
        {
            return string.Join(_delimiter,
                patient.Id,
                patient.Username,
                patient.FirstName,
                patient.LastName,
                patient.Mail,
                patient.Adress,
                patient.Jmbg,
                patient.PhoneNumber);
        }

        private void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }

    }
}