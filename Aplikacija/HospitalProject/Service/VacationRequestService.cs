using HospitalProject.DataTransferObjects;
using HospitalProject.Model;
using HospitalProject.Repository;
using HospitalProject.ValidationRules.DoctorValidation;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class VacationRequestService
    {
        private VacationRequestRepository vacationRequestRepository;

        public VacationRequestService(VacationRequestRepository vacationRequestRepository)
        {
            this.vacationRequestRepository=vacationRequestRepository;
        }

        public bool Create(NewRequestDTO newRequestDTO)
        {

            if(!CheckIfDoctorAlreadyMadeRequestForGivenDateInterval(newRequestDTO.Doctor, newRequestDTO.StartDate, newRequestDTO.EndDate))
            {
                return CreateRequestIfDateIntervalIsValid(newRequestDTO);
            }

            return false;            
        }

        private bool CreateRequestIfDateIntervalIsValid(NewRequestDTO newRequestDTO)
        {
            if (newRequestDTO.IsUrgent)
            {
                return CreateNewRequest(newRequestDTO);
            }
            else
            {
                return CreateRequestIfNoMoreThanTwoDoctorsAreOnVacation(newRequestDTO);
            }
        }

        private bool CreateNewRequest(NewRequestDTO newRequestDTO)
        {
            VacationRequest vacationRequest = new VacationRequest(newRequestDTO.SubmissionDate, newRequestDTO.Doctor, newRequestDTO.StartDate, newRequestDTO.EndDate, newRequestDTO.Description, newRequestDTO.IsUrgent);
            vacationRequestRepository.Insert(vacationRequest);
            return true;
        }

        private bool CreateRequestIfNoMoreThanTwoDoctorsAreOnVacation(NewRequestDTO newRequestDTO)
        {
            List<VacationRequest> vacationRequestList = vacationRequestRepository.GetVacationRequestsBySpecializationInDateInterval(newRequestDTO.Doctor,newRequestDTO.Doctor.Specialization, newRequestDTO.StartDate, newRequestDTO.EndDate);

            if (VacationRequestValidation.CanCreateNewVacationRequest(vacationRequestList))
            {
                return CreateNewRequest(newRequestDTO);
            }

            return false;
        }

        public List<VacationRequest> GetVacationRequestsForDoctor(Doctor doctor)
        {
            return vacationRequestRepository.GetVacationRequestsForDoctor(doctor);
        }

        private bool CheckIfDoctorAlreadyMadeRequestForGivenDateInterval(Doctor doctor, DateTime StartDate, DateTime EndDate)
        {
            List<VacationRequest> doctorsRequests = vacationRequestRepository.GetVacationRequestsByDoctorInDateInterval(doctor, StartDate, EndDate);

            return doctorsRequests.Any();
        }
    }
}
