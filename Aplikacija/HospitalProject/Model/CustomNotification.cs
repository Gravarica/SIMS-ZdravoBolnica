using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class CustomNotification : ViewModelBase
    {
        private int id;
        private int patientId;
        private DateTime startDate;
        private int interval;
        private string text;

        public CustomNotification(int id,int _patientId, DateTime _startDate, int _interval,  string _text)
        {

            Id = id;
            PatientId = _patientId;
            StartDate = _startDate;
            Interval = _interval;
            Text = _text;


        }

        public CustomNotification(int _patientId, DateTime _startDate, int _interval, string _text)
        {

            PatientId= _patientId;
            StartDate = _startDate;
            Interval = _interval;
            Text = _text;


        }


        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged(nameof(Id));
            }

        }

        public int PatientId
        {
            get
            {
                return patientId;
            }
            set
            {
                this.patientId = value;
                OnPropertyChanged(nameof(PatientId));
            }

        }

       

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                this.startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }

        }


        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                this.interval = value;
                OnPropertyChanged(nameof(Interval));
            }

        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                this.text = value;
                OnPropertyChanged(nameof(Text));
            }

        }




    }
}
