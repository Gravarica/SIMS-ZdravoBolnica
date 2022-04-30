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
            _path=path;
            _delimiter=delimiter;
            _datetimeFormat=dateTimeFormat;
        }

        private Patient ConvertCSVFormatToPatient(string acountCSVFormat)                   
        {
            /*
             0id
             1medical record
             2BloodType bloodtype, 
            3bool guest,
            4String username,
            5String firstName,
            6String lastName,
            7UserType userType,
            8int jmbg,
            9int phoneNumber,
            10string email,
            11string adress,
            12DateTime dateofBirth,
            13Gender gender*/
            string[] tokens = acountCSVFormat.Split(_delimiter.ToCharArray());
            return new Patient(int.Parse(tokens[0]),
                int.Parse(tokens[1]),
                (BloodType)Enum.Parse(typeof(BloodType), tokens[2], true),
                bool.Parse(tokens[3]),
                tokens[4],
                tokens[5],
                tokens[6],
                (UserType)Enum.Parse(typeof(UserType), tokens[7], true),
                int.Parse(tokens[8]),
                int.Parse(tokens[9]),
                tokens[10],
                tokens[11],
                DateTime.Parse(tokens[12]),
                (Gender)Enum.Parse(typeof(Gender), tokens[13], true));
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
                patient.BloodType,
                patient.Guest,
                patient.Username,
                patient.FirstName,
                patient.LastName,
                patient.UserType,
                patient.Jmbg,
                patient.PhoneNumber,
                patient.Email,
                patient.Adress,
                patient.DateOfBirth.ToString(_datetimeFormat),
                patient.Gender
            );
        }

        public void AppendLineToFile(string path, string line)
        {
            File.AppendAllText(path, line + Environment.NewLine);
        }
    }
}