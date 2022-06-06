using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class CustomNotificationFileHandler : GenericFileHandler<CustomNotification>
    {

        public CustomNotificationFileHandler(string path) : base(path) {}

        protected override string ConvertEntityToCSV(CustomNotification customNotification)
        {
            return string.Join(CSV_DELIMITER,
                customNotification.Id,
                customNotification.PatientId,
                customNotification.StartDate.ToString("MM/dd/yyyy HH:mm"),
                customNotification.Interval,
                customNotification.Text
            );
        }

        protected override CustomNotification ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER);
            return new CustomNotification(int.Parse(tokens[0]),
                int.Parse(tokens[1]),
                DateTime.ParseExact(tokens[2], "MM/dd/yyyy HH:mm", null),
                int.Parse(tokens[3]),
                tokens[4]);
        }
    }
}
