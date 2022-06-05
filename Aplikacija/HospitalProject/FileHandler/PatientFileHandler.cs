using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class PatientFileHandler : GenericFileHandler<Patient>
    {
        public PatientFileHandler(string path) : base(path)
        {
        }

        protected override string ConvertEntityToCSV(Patient patient)
        {
            return string.Join(CSV_DELIMITER,
                patient.Id,
                patient.MedicalRecordId,
                patient.Guest,
                patient.Username,
                patient.FirstName,
                patient.LastName,
                patient.Jmbg,
                patient.PhoneNumber,
                patient.Email,
                patient.Adress,
                patient.DateOfBirth.ToString(FormatStorage.DATETIME_FORMAT),
                patient.Gender);
        }

        protected override Patient ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER.ToCharArray());
            return new Patient(
                int.Parse(tokens[0]),
                int.Parse(tokens[1]),
                bool.Parse(tokens[2]),
                tokens[3],
                tokens[4],
                tokens[5],
                int.Parse(tokens[6]),
                int.Parse(tokens[7]),
                tokens[8],
                tokens[9],
                DateTime.Parse(tokens[10]),
                (Gender)Enum.Parse(typeof(Gender), tokens[11], true));
        }
    }
}