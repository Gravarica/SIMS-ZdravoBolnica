using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.DataUtility;

namespace HospitalProject.FileHandler
{
    public class VacationRequestFileHandler : GenericFileHandler<VacationRequest>
    {
        private string _dateTimeFormat;

        public VacationRequestFileHandler(string path) : base(path)
        {
            _dateTimeFormat = FormatStorage.ONLY_DATE_FORMAT;
        }


        protected override VacationRequest ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER);

            return new VacationRequest(int.Parse(tokens[0]),
                DateTime.ParseExact(tokens[1], _dateTimeFormat, null),
                int.Parse(tokens[2]),
                DateIntervalCreator.CreateDateInterval(tokens[3], tokens[4]),
                tokens[5],
                bool.Parse(tokens[6]),
                EnumConverter.ConvertTokenToRequestState(tokens[7]),
                tokens[8]);
        }

        protected override string ConvertEntityToCSV(VacationRequest vacationRequest)
        {
            return string.Join(CSV_DELIMITER,
                vacationRequest.Id,
                vacationRequest.SubmissionDate.ToString(_dateTimeFormat),
                vacationRequest.Doctor.Id,
                vacationRequest.DateInterval.StartDate.ToString(_dateTimeFormat),
                vacationRequest.DateInterval.EndDate.ToString(_dateTimeFormat),
                vacationRequest.Description,
                vacationRequest.IsUrgent.ToString(),
                vacationRequest.RequestState.ToString(),
                vacationRequest.SecretaryDescription);
        }

    }
}
