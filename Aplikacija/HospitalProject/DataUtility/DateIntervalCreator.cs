using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.FileHandler;
using HospitalProject.Model;

namespace HospitalProject.DataUtility
{
    public static class DateIntervalCreator
    {
        public static DateInterval CreateDateInterval(string startDateString, string endDateString)
        {
            DateTime startDate = DateTime.ParseExact(startDateString, FormatStorage.ONLY_DATE_FORMAT, null);
            DateTime endDate = DateTime.ParseExact(endDateString, FormatStorage.ONLY_DATE_FORMAT, null);
            return new DateInterval(startDate, endDate);
        }
    }
}
