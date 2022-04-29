using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class PrescriptionFileHandler
    {

        private string _path;

        private string _delimiter;

        public PrescriptionFileHandler(String path, String delimiter)
        {
            _path = path;
            _delimiter = delimiter;
        }

        public IEnumerable<Prescription> ReadAll()
        {
            return File.ReadAllLines(_path)                 // Radi tako sto, procitamo sve linije iz fajla, i svaku od tih linija prebacimo iz CSV formata u entitet i toList()
                   .Select(ConvertCSVFormatToPrescription)   // 1 | 20.01.2000 12:15| 20 | 2 | 3 => app1(...) 
                   .ToList();
        }

        public Prescription ConvertCSVFormatToPrescription(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new Prescription(int.Parse(tokens[0]),
                                   int.Parse(tokens[1]),
                                   int.Parse(tokens[2]),
                                   DateOnly.Parse(tokens[3]),
                                   DateOnly.Parse(tokens[4]),
                                   int.Parse(tokens[5]));
        }

        public string ConvertPrescriptionToCSVFormat(Prescription prescription)
        {
            return string.Join(_delimiter,
            prescription.Id,
            prescription.Patient.Id,
            prescription.Doctor.Id,
            prescription.StartDate.ToString(),
            prescription.EndDate.ToString(),
            prescription.Interval);
        }

        public void AppendLineToFile(Prescription prescription)
        {
            string line = ConvertPrescriptionToCSVFormat(prescription);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public void Save(IEnumerable<Prescription> prescriptions)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (Prescription prescription in prescriptions)
                {
                    file.WriteLine(ConvertPrescriptionToCSVFormat(prescription));
                }
            }
        }

    }
}
