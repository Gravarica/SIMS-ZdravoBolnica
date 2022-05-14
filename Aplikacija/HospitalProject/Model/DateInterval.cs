using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class DateInterval : ViewModelBase
    {
        private DateTime startDate;
        private DateTime endDate;

        public DateInterval(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;  
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public bool Overlaps(DateInterval compareDateInterval)
        {
            return compareDateInterval.StartDate <= EndDate && StartDate <= compareDateInterval.EndDate;
        }

    }
}
