using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.Model
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        int _appointmentId;
        DateTime _date;
        int _duration;
        int _patientId;
        string _patientName;
        int _doctorId;
        string _doctorName;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int AppointmentId 
        { 
            get
            { 
                return _appointmentId; 
            }
            set
            {
                if (value != _appointmentId)
                {
                    _appointmentId = value;
                    OnPropertyChanged(nameof(AppointmentId));
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if(value != _date)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if(value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        public int PatientId
        {
            get
            {
                return _patientId;
            }
            set
            {
                if(value!=_patientId)
                {
                    _patientId = value;
                    OnPropertyChanged(nameof(PatientId));
                }
            }
        }

        public string PatientName
        {
            get
            {
                return _patientName;
            }
            set
            {
                if(value != _patientName)
                {
                    _patientName = value;
                    OnPropertyChanged(nameof(PatientName));
                }
            }
        }

        public int DoctorId
        {
            get
            {
                return _doctorId;
            }
            set
            {
                if (value != _doctorId)
                {
                    _doctorId = value;
                    OnPropertyChanged(nameof(DoctorId));
                }
            }
        }

        public string DoctorName
        {
            get
            {
                return _doctorName;
            }
            set
            {
                if (value != _doctorName)
                {
                    _doctorName = value;
                    OnPropertyChanged(nameof(DoctorName));
                }
            }
        }
    }
}
