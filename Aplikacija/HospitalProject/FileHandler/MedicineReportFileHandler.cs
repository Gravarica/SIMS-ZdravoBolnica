using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class MedicineReportFileHandler
    {
        string _path;
        string _delimiter;
        string _dateTimeFormat;

        public MedicineReportFileHandler(string path, string delimiter, string dateTimeFormat)
        {
            _path=path;
            _delimiter=delimiter;
            _dateTimeFormat=dateTimeFormat;
        }

        private string ConvertReportToCSVFormat(MedicineReport medicineReport)
        {
            return string.Join(_delimiter,
                               medicineReport.Id,
                               medicineReport.Doctor.Id,
                               medicineReport.SubmissionDate.ToString(_dateTimeFormat),
                               medicineReport.Medicine.Id,
                               medicineReport.Description);
        }

        private MedicineReport ConvertCSVFormatToReport(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(_delimiter);
            return new MedicineReport(int.Parse(tokens[0]),
                                      int.Parse(tokens[1]),
                                      DateTime.ParseExact(tokens[2], _dateTimeFormat, null),
                                      int.Parse(tokens[3]),
                                      tokens[4]);
        }

        public void AppendLineToFile(MedicineReport medicineReport)
        {
            string line = ConvertReportToCSVFormat(medicineReport);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public IEnumerable<MedicineReport> ReadAll()
        {
            return File.ReadAllLines(_path)
                   .Select(ConvertCSVFormatToReport)
                   .ToList();
        }

        public void Save(IEnumerable<MedicineReport> medicineReports)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (MedicineReport medicineReport in medicineReports)
                {
                    file.WriteLine(ConvertReportToCSVFormat(medicineReport));
                }
            }
        }
    }
}
