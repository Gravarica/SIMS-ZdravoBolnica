using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class NotificationFileHandler : GenericFileHandler<Notification>
    {

        public NotificationFileHandler(string path) : base(path) {}

        protected override string ConvertEntityToCSV(Notification notification)
        {
            return string.Join(CSV_DELIMITER,
                notification.Id,
                notification.Name,
                notification.Prescription.Id,
                notification.StartTime.ToString("HH:mm")
            );
        }

        protected override Notification ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER);
            return new Notification(int.Parse(tokens[0]),
                tokens[1],
                int.Parse(tokens[2]),
                DateTime.ParseExact(tokens[3], "HH:mm", null));
        }
    }
}
