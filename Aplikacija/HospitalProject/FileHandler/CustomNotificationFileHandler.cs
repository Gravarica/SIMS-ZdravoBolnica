using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class CustomNotificationFileHandler
    {
        private string path;
        private string delimiter;

        public CustomNotificationFileHandler(string path, string delimiter)
        {
            this.path = path;
            this.delimiter = delimiter;
        }

        public IEnumerable<CustomNotification> ReadAll()
        {
            return File.ReadAllLines(path).Select(ConvertCSVToCustomNotification).ToList();
        }

        private string ConvertCustomNotificationToCSV(CustomNotification customNotification)
        {
            return string.Join(delimiter,
                               customNotification.Id,
                               customNotification.PatientId,
                               customNotification.StartDate.ToString("MM/dd/yyyy HH:mm"),
                               customNotification.Interval,
                               customNotification.Text
                               );
        }

        private CustomNotification ConvertCSVToCustomNotification(string csv)
        {
            string[] tokens = csv.Split(delimiter);
            return new CustomNotification(int.Parse(tokens[0]),
                                          int.Parse(tokens[1]),   
                                          DateTime.ParseExact(tokens[2], "MM/dd/yyyy HH:mm", null),
                                          int.Parse(tokens[3]),
                                          tokens[4]);
        }

        public void AppendLineToFile(CustomNotification customNotification)
        {
            string line = ConvertCustomNotificationToCSV(customNotification);
            File.AppendAllText(path, line + Environment.NewLine);
        }

        public void Save(List<CustomNotification> customNotifications)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                foreach (CustomNotification customNotification in customNotifications)
                {
                    file.WriteLine(ConvertCustomNotificationToCSV(customNotification));
                }
            }
        }



    }
}
