using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class MedicalRecordFileHandler
    {
        private readonly string _path;

        private readonly string _delimiter;

        public MedicalRecordFileHandler(string path, string delimiter)
        {
            _path=path;

            _delimiter=delimiter;
        }

        // I only want to save ID | patientID
        public string ConvertMedicalRecordToCSVFormat(MedicalRecord medicalRecord)
        {
            return string.Join(_delimiter,
                               medicalRecord.Id,
                               medicalRecord.Patient.Id);
        }

        public MedicalRecord ConvertCSVFormatToMedicalRecord(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new MedicalRecord(int.Parse(tokens[0]), int.Parse(tokens[1]));
        }

        public IEnumerable<MedicalRecord> ReadAll()
        {
            return File.ReadAllLines(_path)                 
                   .Select(ConvertCSVFormatToMedicalRecord)   
                   .ToList();
        }

        public void Save(IEnumerable<MedicalRecord> medicalRecords)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (MedicalRecord medicalRecord in medicalRecords)
                {
                    file.WriteLine(ConvertMedicalRecordToCSVFormat(medicalRecord));
                }
            }
        }

        public void AppendLineToFile(MedicalRecord medicalRecord)
        {
            string line = ConvertMedicalRecordToCSVFormat(medicalRecord);
            File.AppendAllText(_path, line + Environment.NewLine);
        }
    }
}
