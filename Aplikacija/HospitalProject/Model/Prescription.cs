using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class Prescription : ViewModelBase
    {
        private int id;
        private Patient patient;
        private Doctor doctor;
        // Ovde treba i lek cuvati 
        private DateOnly startDate;
        private DateOnly endDate;
        private int interval;
        private TimeOnly startTime;

        public Prescription(int id, Patient patient, Doctor doctor, DateOnly startDate, DateOnly endDate, int interval)
        {
            this.id = id;
            this.patient = patient;
            this.doctor = doctor;
            this.startDate = startDate;
            this.endDate = endDate;
            this.Interval = interval;
        }

        public Prescription(int id, int patientId, int doctorId, DateOnly startDate, DateOnly endDate, int interval)
        {
            Id = id;
            Patient = new Patient();
            Patient.Id = patientId;
            Doctor = new Doctor();
            Doctor.Id = doctorId;
            StartDate = startDate;
            EndDate = endDate;
            Interval = interval;
        }

        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
                OnPropertyChanged(nameof(Interval));
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                OnPropertyChanged(nameof(Doctor));
            }
        }

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }

        public DateOnly StartDate
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

        public DateOnly EndDate
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

        public TimeOnly StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }
    }
}
