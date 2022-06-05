﻿using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.FileHandler
{
    public class VacationRequestFileHandler : GenericFileHandler<VacationRequest>
    {

        private string _path;
        private string _dateTimeFormat;

        public VacationRequestFileHandler(string path) : base(path)
        {
            _path = path;
            _dateTimeFormat = FormatStorage.ONLY_DATE_FORMAT;
        }

        /*private VacationRequest ConvertCSVFormatToVacationRequest(string csvString)
        {
            string[] tokens = csvString.Split(CSV_DELIMITER);

            return new VacationRequest(int.Parse(tokens[0]),
                                       DateTime.ParseExact(tokens[1], _dateTimeFormat, null),
                                       int.Parse(tokens[2]),
                                       CreateDateInterval(tokens[3], tokens[4]),  
                                       tokens[5],
                                       bool.Parse(tokens[6]),
                                       ConvertTokenToRequestState(tokens[7]),
                                       tokens[8]);

        }*/

        protected override VacationRequest ConvertCSVToEntity(string csv)
        {
            string[] tokens = csv.Split(CSV_DELIMITER);

            return new VacationRequest(int.Parse(tokens[0]),
                DateTime.ParseExact(tokens[1], _dateTimeFormat, null),
                int.Parse(tokens[2]),
                CreateDateInterval(tokens[3], tokens[4]),
                tokens[5],
                bool.Parse(tokens[6]),
                ConvertTokenToRequestState(tokens[7]),
                tokens[8]);
        }

        private DateInterval CreateDateInterval(string startDateString, string endDateString)
        {
            DateTime startDate = DateTime.ParseExact(startDateString, _dateTimeFormat, null);
            DateTime endDate = DateTime.ParseExact(endDateString, _dateTimeFormat, null);
            return new DateInterval(startDate, endDate);    
        }

        /*private string ConvertVacationRequestToCSVFormat(VacationRequest vacationRequest)
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
        }*/

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

        /*public IEnumerable<VacationRequest> ReadAll()
        {
            return File.ReadAllLines(_path)
                   .Select(ConvertCSVFormatToVacationRequest)
                   .ToList();
        }*/

        /*public void AppendLineToFile(VacationRequest vacationRequest)
        {
            string line = ConvertVacationRequestToCSVFormat(vacationRequest);
            File.AppendAllText(_path, line + Environment.NewLine);
        }

        public void Save(IEnumerable<VacationRequest> vacationRequests)
        {
            using StreamWriter file = new StreamWriter(_path);
            foreach (VacationRequest appointment in vacationRequests)
            {
                file.WriteLine(ConvertVacationRequestToCSVFormat(appointment));
            }
        }*/

        private RequestState ConvertTokenToRequestState(string token)
        {
            if (token.Equals("APPROVED"))
            {
                return RequestState.APPROVED;
            }
            else if (token.Equals("DENIED"))
            {
                return RequestState.DENIED;
            }

            return RequestState.PENDING;
        }

    }
}
