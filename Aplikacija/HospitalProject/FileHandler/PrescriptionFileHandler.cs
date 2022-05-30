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
        private string _dateTimeFormat;

        public PrescriptionFileHandler(string path, string delimiter, string dateTimeFormat)
        {
            _path = path;
            _delimiter = delimiter;
            _dateTimeFormat = dateTimeFormat;
        }

        public IEnumerable<Prescription> ReadAll()
        {
            return File.ReadAllLines(_path)                 
                   .Select(ConvertCSVFormatToPrescription)  
                   .ToList();
        }

        public Prescription ConvertCSVFormatToPrescription(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new Prescription(int.Parse(tokens[0]),
                                   int.Parse(tokens[1]),
                                   DateTime.ParseExact(tokens[2], _dateTimeFormat, null),
                                   DateTime.ParseExact(tokens[3], _dateTimeFormat, null),
                                   int.Parse(tokens[4]),
                                   tokens[5],
                                   int.Parse(tokens[6]));
        }

        public string ConvertPrescriptionToCSVFormat(Prescription prescription)
        {
            return string.Join(_delimiter,
            prescription.Id,
            prescription.Appointment.Id,
            prescription.StartDate.ToString(),
            prescription.EndDate.ToString(),
            prescription.Interval,
            prescription.Description,
            prescription.Medicine.Id);
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
