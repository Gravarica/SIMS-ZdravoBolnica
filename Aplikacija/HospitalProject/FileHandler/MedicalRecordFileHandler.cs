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

        private const string ALLERGY_CSV = "-";
       
        public MedicalRecordFileHandler(string path, string delimiter)
        {
            _path=path;

            _delimiter=delimiter;
        }

        // I only want to save ID | patientID
        public string ConvertMedicalRecordToCSVFormat(MedicalRecord medicalRecord)
        {
            if (medicalRecord.Allergies.Count == 0)
            {
                return string.Join(_delimiter,
                    medicalRecord.Id,
                    medicalRecord.Patient.Id);
            }

            return string.Join(_delimiter,
                medicalRecord.Id,
                medicalRecord.Patient.Id,
                ConvertAllergensToCSV(medicalRecord.Allergies));
        }

        public MedicalRecord ConvertCSVFormatToMedicalRecord(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());

            if (tokens.Length == 3)
            {
                return new MedicalRecord(int.Parse(tokens[0]), 
                    int.Parse(tokens[1]),
                    GetAllergiesFromCSV(tokens[2]));
            }
            
            return new MedicalRecord(int.Parse(tokens[0]), 
                                     int.Parse(tokens[1]),
                                     new List<Allergies>());
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

        private List<Allergies> GetAllergiesFromCSV(string CSVToken)
        {
            
            string[] allergyIds = CSVToken.Split(ALLERGY_CSV);
            List<Allergies> allergies = new List<Allergies>();
            int lenght = allergyIds.Length;
            
             for(int i = 0; i < lenght; i++)
            {
              AddAllergenToList(allergies, int.Parse(allergyIds[i]));
            } 
               
           return allergies;
           
        }


        private void AddAllergenToList(List<Allergies> allergens, int allergenId)
        {
            Allergies allergen = new Allergies(allergenId);
            allergens.Add(allergen);
        }

        private string ConvertAllergensToCSV(List<Allergies> allergens)
        {
            if(allergens.Count == 0)
            {
                return null;
            }

            string CSVOutput = allergens.ElementAt(0).Id.ToString();

            for(int i = 1; i < allergens.Count; i++)
            {
                CSVOutput = ALLERGY_CSV + allergens.ElementAt(i).Id.ToString(); 
            }

            return CSVOutput;
        }
    }
}
