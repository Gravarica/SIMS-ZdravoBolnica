using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class PrescriptionFileHandler : GenericFileHandler<Prescription>
    {

        public PrescriptionFileHandler(string path) : base(path) { }

        /*public IEnumerable<Prescription> ReadAll()
        {
            return File.ReadAllLines(_path)                 
                   .Select(ConvertCSVFormatToPrescription)  
                   .ToList();
        }*/

        protected override Prescription ConvertCSVToEntity(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(CSV_DELIMITER.ToCharArray());
            return new Prescription(int.Parse(tokens[0]),
                                   int.Parse(tokens[1]),
                                   DateOnly.Parse(tokens[2]),
                                   DateOnly.Parse(tokens[3]),
                                   int.Parse(tokens[4]),
                                   tokens[5],
                                   int.Parse(tokens[6]));
        }

        protected override string ConvertEntityToCSV(Prescription prescription)
        {
            return string.Join(CSV_DELIMITER,
            prescription.Id,
            prescription.Appointment.Id,
            prescription.StartDate.ToString(),
            prescription.EndDate.ToString(),
            prescription.Interval,
            prescription.Description,
            prescription.Medicine.Id);
        }

        /*public void AppendLineToFile(Prescription prescription)
        {
            string line = ConvertPrescriptionToCSVFormat(prescription);
           *File.AppendAllText(_path, line + Environment.NewLine);
        }*/

       /* public void Save(IEnumerable<Prescription> prescriptions)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (Prescription prescription in prescriptions)
                {
                    file.WriteLine(ConvertPrescriptionToCSVFormat(prescription));
                }
            }
        }*/

    }
}
