using HospitalProject.Model;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class AppointmentFileHandler : GenericFileHandler<Appointment>
    {

        private readonly string _datetimeFormat;

        public AppointmentFileHandler(string path) : base(path)
        {
            _datetimeFormat=FormatStorage.ONLY_DATE_FORMAT;
        }

        protected override Appointment ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER.ToCharArray());
            return new Appointment(int.Parse(tokens[0]),
                DateTime.Parse(tokens[1]),
                int.Parse(tokens[2]),
                int.Parse(tokens[3]),
                int.Parse(tokens[4]),
                int.Parse(tokens[5]),
                convertToExaminationType(tokens[6]),
                bool.Parse(tokens[7]));
        }

       /* private Appointment ConvertCSVFormatToAppointment(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(CSV_DELIMITER.ToCharArray());
            return new Appointment(int.Parse(tokens[0]),
                                   DateTime.Parse(tokens[1]),
                                   int.Parse(tokens[2]),
                                   int.Parse(tokens[3]),
                                   int.Parse(tokens[4]),
                                   int.Parse(tokens[5]),
                                   convertToExaminationType(tokens[6]),
                                   bool.Parse(tokens[7]));
        }*/

       protected override string ConvertEntityToCSV(Appointment appointment)
       {
           return string.Join(CSV_DELIMITER,
               appointment.Id,
               appointment.Date.ToString(_datetimeFormat),
               appointment.Duration,
               appointment.Patient.Id,
               appointment.Doctor.Id,
               appointment.Room.Id,
               appointment.ExaminationType.ToString(),
               appointment.IsDone.ToString());
        }

      /* private string ConvertAppointmentToCSVFormat(Appointment appointment)
        {
            return string.Join(CSV_DELIMITER,
                appointment.Id,
                appointment.Date.ToString(_datetimeFormat),
                appointment.Duration,
                appointment.Patient.Id,
                appointment.Doctor.Id,
                appointment.Room.Id,
                appointment.ExaminationType.ToString(),
                appointment.IsDone.ToString());
        }*/

        private ExaminationType convertToExaminationType(string examinationTypeString)
        {
            if(examinationTypeString.Equals("OPERATION"))
            {
                return ExaminationType.OPERATION;
            } 
            else if (examinationTypeString.Equals("GENERAL"))
            {
                return ExaminationType.GENERAL;
            }
            else if (examinationTypeString.Equals("IMAGING"))
            {
                return ExaminationType.IMAGING;
            }

            return ExaminationType.GENERAL;
        }
    }
}
