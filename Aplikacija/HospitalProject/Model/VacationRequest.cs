using HospitalProject.DataTransferObjects;
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
        private DateInterval dateInterval;
        private string description;
        private bool isUrgent;
        private RequestState requestState;

        // Konstruktor za kreiranje objekta iz file handlera
        public VacationRequest(int id, DateTime submissionDate, int doctorId, DateInterval dateInterval, string description, bool isUrgent, RequestState requestState)
        {
            Id = id;
            InstantiateDoctor(doctorId);
            InstantiateData(submissionDate,dateInterval,description,isUrgent,requestState);
        }

        // Konstruktor za kreiranje objekta sa fronta
        public VacationRequest(NewRequestDTO newRequestDTO)
        {
            Doctor = newRequestDTO.Doctor;
            InstantiateData(newRequestDTO.SubmissionDate, newRequestDTO.DateInterval, newRequestDTO.Description, newRequestDTO.IsUrgent, RequestState.PENDING);
        }

        private void InstantiateDoctor(int doctorId)
        {
            Doctor = new Doctor();
            Doctor.Id = doctorId;
        }

        private void InstantiateData(DateTime submissionDate, DateInterval dateInterval, string description, bool isUrgent, RequestState requestState)
        {
            SubmissionDate = submissionDate;
            DateInterval = dateInterval;   
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

        public DateInterval DateInterval
        {
            get
            {
                return dateInterval;
            }
            set
            {
                dateInterval = value;
                OnPropertyChanged(nameof(DateInterval));
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

        public bool Overlaps(DateTime intervalStart, DateTime intervalEnd)
        {
            return intervalStart <= DateInterval.EndDate && DateInterval.StartDate <= intervalEnd;
        }

        public bool HasDistinctDoctorForSpecialization(Doctor givenDoctor)
        {
            return SpecializationMatches(givenDoctor.Specialization) && !DoctorMatches(givenDoctor);
        }

        public bool SpecializationMatches(Specialization specialization)
        {
            return Doctor.Specialization == specialization;
        }

        public bool DoctorMatches(Doctor givenDoctor)
        {
            return Doctor.Id == givenDoctor.Id;
        }

        public bool RequestStateMatches(RequestState givenRequestState)
        {
            return RequestState == givenRequestState;
        }
    }
}
