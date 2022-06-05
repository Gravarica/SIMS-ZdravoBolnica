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


        protected override Prescription ConvertCSVToEntity(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(CSV_DELIMITER.ToCharArray());
            return new Prescription(int.Parse(tokens[0]),
                                   int.Parse(tokens[1]),
                                   DateTime.ParseExact(tokens[2], FormatStorage.ONLY_DATE_FORMAT, null),
                                   DateTime.ParseExact(tokens[3], FormatStorage.ONLY_DATE_FORMAT, null),
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

    }
}
