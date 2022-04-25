using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class AppointmentFileHandler
    {

        private readonly string _path;

        private readonly string _delimiter;

        private readonly string _datetimeFormat;

        public AppointmentFileHandler(string path, string delimiter, string dateTimeFormat)
        {
            _path=path;
            _delimiter=delimiter;
            _datetimeFormat=dateTimeFormat;
        }
        
        public IEnumerable<Appointment> ReadAll()
        {
            return File.ReadAllLines(_path)                 
                   .Select(ConvertCSVFormatToAppointment)   
                   .ToList();
        }

        private Appointment ConvertCSVFormatToAppointment(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter.ToCharArray());
            return new Appointment(int.Parse(tokens[0]),
                                   DateTime.Parse(tokens[1]),
                                   int.Parse(tokens[2]),
                                   int.Parse(tokens[3]),
                                   int.Parse(tokens[4]),
                                   int.Parse(tokens[5]));
        }

        private string ConvertAppointmentToCSVFormat(Appointment appointment)
        {
            return string.Join(_delimiter,
                appointment.Id,
                appointment.Date.ToString(_datetimeFormat),
                appointment.Duration,
                appointment.Patient.Id,
                appointment.Doctor.Id,
                appointment.Room.Id);
        }

        public void AppendLineToFile(Appointment appointment)
        {
            string line = ConvertAppointmentToCSVFormat(appointment);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public void Save(IEnumerable<Appointment> _appointments)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (Appointment appointment in _appointments)
                {
                    file.WriteLine(ConvertAppointmentToCSVFormat(appointment));
                }
            }
        }
    }
}
