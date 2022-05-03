using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class PatientFileHandler
    {
        private List<Patient> _patients = new List<Patient>();
        private string _path;

        private string _delimiter;
        
        private readonly string _datetimeFormat;
        
        public PatientFileHandler(string path, string delimiter, string dateTimeFormat)
        {
            _path = path;
            _delimiter=delimiter;
            _datetimeFormat=dateTimeFormat;
        }

        private Patient ConvertCSVFormatToPatient(string acountCSVFormat)                   
        {
            string[] tokens = acountCSVFormat.Split(_delimiter.ToCharArray());
            return new Patient(
                int.Parse(tokens[0]),
                int.Parse(tokens[1]),
                bool.Parse(tokens[2]),
                tokens[3],
                tokens[4],
                tokens[5],
                tokens[6],
                int.Parse(tokens[7]),
                int.Parse(tokens[8]),
                tokens[9],
                tokens[10],
                DateTime.Parse(tokens[11]),
                (Gender)Enum.Parse(typeof(Gender), tokens[12], true));
        }

        public IEnumerable<Patient> ReadAll()
        {
            return File.ReadAllLines(_path)             
                .Select(ConvertCSVFormatToPatient)   
                .ToList();
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

        public string ConvertPatientToCSVFormat(Patient patient)
        { 
            return string.Join(_delimiter,
                patient.Id,
                patient.MedicalRecordId,
                patient.Guest,
                patient.Username,
                patient.Password,
                patient.FirstName,
                patient.LastName,
                patient.Jmbg,
                patient.PhoneNumber,
                patient.Email,
                patient.Adress,
                patient.DateOfBirth.ToString(_datetimeFormat),
                patient.Gender);
        }

        public void AppendLineToFile(Patient Patient)
        {
            string line = ConvertPatientToCSVFormat(Patient);
            File.AppendAllText(_path, Environment.NewLine + line);
        }
        
    }
}