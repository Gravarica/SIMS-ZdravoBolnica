using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class MedicineReportFileHandler : GenericFileHandler<MedicineReport>
    {
        string _dateTimeFormat;

        public MedicineReportFileHandler(string path) : base(path)
        {
            _dateTimeFormat=FormatStorage.ONLY_DATE_FORMAT;
        }

        protected override MedicineReport ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER);
            return new MedicineReport(int.Parse(tokens[0]),
                int.Parse(tokens[1]),
                DateTime.ParseExact(tokens[2], _dateTimeFormat, null),
                int.Parse(tokens[3]),
                tokens[4]);
        }

        protected override string ConvertEntityToCSV(MedicineReport medicineReport)
        {
            return string.Join(CSV_DELIMITER,
                medicineReport.Id,
                medicineReport.Doctor.Id,
                medicineReport.SubmissionDate.ToString(_dateTimeFormat),
                medicineReport.Medicine.Id,
                medicineReport.Description);
        }

        /*private string ConvertReportToCSVFormat(MedicineReport medicineReport)
        {
            return string.Join(CSV_DELIMITER,
                               medicineReport.Id,
                               medicineReport.Doctor.Id,
                               medicineReport.SubmissionDate.ToString(_dateTimeFormat),
                               medicineReport.Medicine.Id,
                               medicineReport.Description);
        }

        private MedicineReport ConvertCSVFormatToReport(string CSVFormat)
        {
            string[] tokens = CSVFormat.Split(CSV_DELIMITER);
            return new MedicineReport(int.Parse(tokens[0]),
                                      int.Parse(tokens[1]),
                                      DateTime.ParseExact(tokens[2], _dateTimeFormat, null),
                                      int.Parse(tokens[3]),
                                      tokens[4]);
        }*/

        /*public void AppendLineToFile(MedicineReport medicineReport)
        {
            string line = ConvertReportToCSVFormat(medicineReport);
            File.AppendAllText(_path, line + Environment.NewLine);
        }*/

       /* public IEnumerable<MedicineReport> ReadAll()
        {
            return File.ReadAllLines(_path)
                   .Select(ConvertCSVFormatToReport)
                   .ToList();
        }*/

        /*public void Save(IEnumerable<MedicineReport> medicineReports)
        {
            using (StreamWriter file = new StreamWriter(_path))
            {
                foreach (MedicineReport medicineReport in medicineReports)
                {
                    file.WriteLine(ConvertReportToCSVFormat(medicineReport));
                }
            }
        }*/
    }
}
