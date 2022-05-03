using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class NotificationFileHandler
    {

        private string path;
        private string delimiter;

        public NotificationFileHandler(string path, string delimiter)
        {
            this.path = path;
            this.delimiter = delimiter;
        }

        public IEnumerable<Notification> ReadAll()
        {
            return File.ReadAllLines(path).Select(ConvertCSVToNotification).ToList();
        }

        private string ConvertNotificationToCSV(Notification notification)
        {
            return string.Join(delimiter,
                               notification.Id,
                               notification.Name,
                               notification.Prescription.Id,
                               notification.StartTime.ToString()
                               ) ;
        }

        private Notification ConvertCSVToNotification(string csv)
        {
            string[] tokens = csv.Split(delimiter);
            return new Notification(int.Parse(tokens[0]),
                                    tokens[1],
                                    int.Parse(tokens[2]),
                                    DateTime.ParseExact(tokens[3], "HH:mm", null));
        }

        public void AppendLineToFile(Notification notification)
        {
            string line = ConvertNotificationToCSV(notification);
            File.AppendAllText(path, line + Environment.NewLine);
        }

        public void Save(List<Notification> notifications) 
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                foreach (Notification notification in notifications)
                {
                    file.WriteLine(ConvertNotificationToCSV(notification));
                }
            }
        }
    }
}
