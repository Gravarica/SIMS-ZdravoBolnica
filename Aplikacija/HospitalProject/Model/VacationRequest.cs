using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class VacationRequest : ViewModelBase
    {
        private int id;
        private DateTime submissionDate;
        private Doctor doctor;
        private DateTime startDate;
        private DateTime endDate;
        private string description;
        private bool isUrgent;
        private RequestState requestState;

        // Konstruktor za kreiranje objekta iz file handlera
        public VacationRequest(int id, DateTime submissionDate, int doctorId, DateTime startDate, DateTime endDate, string description, bool isUrgent, RequestState requestState)
        {
            Id = id;
            InstantiateDoctor(doctorId);
            InstantiateData(submissionDate,startDate,endDate,description,isUrgent,requestState);
        }

        // Konstruktor za kreiranje objekta sa fronta
        public VacationRequest(DateTime submissionDate, Doctor doctor, DateTime startDate, DateTime endDate, string description, bool isUrgent, RequestState requestState)
        {
            Doctor = doctor;
            InstantiateData(submissionDate, startDate, endDate, description, isUrgent, requestState);
        }

        private void InstantiateDoctor(int doctorId)
        {
            Doctor = new Doctor();
            Doctor.Id = doctorId;
        }

        private void InstantiateData(DateTime submissionDate, DateTime startDate, DateTime endDate, string description, bool isUrgent, RequestState requestState)
        {
            SubmissionDate = submissionDate;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            IsUrgent = isUrgent;
            RequestState = requestState;
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

        public DateTime SubmissionDate
        {
            get
            {
                return submissionDate;
            }
            set
            {
                submissionDate = value;
                OnPropertyChanged(nameof(SubmissionDate));
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

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public bool IsUrgent
        {
            get
            {
                return isUrgent;
            }
            set
            {
                isUrgent = value;
                OnPropertyChanged(nameof(IsUrgent));
            }
        }

        public RequestState RequestState
        { 
            get
            {
                return requestState;
            }
            set
            {
                requestState = value;
                OnPropertyChanged(nameof(RequestState));
            }
        }
    }
}
