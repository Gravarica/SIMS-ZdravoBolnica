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

            if(!CheckIfDoctorAlreadyMadeRequestForGivenDateInterval(newRequestDTO.Doctor, newRequestDTO.DateInterval))
            {
                return CreateRequestIfDateIntervalIsValid(newRequestDTO);
            }

            return false;            
        }

        private bool CreateRequestIfDateIntervalIsValid(NewRequestDTO newRequestDTO)
        {
            return newRequestDTO.IsUrgent ? CreateNewRequest(newRequestDTO) : CreateRequestIfNoMoreThanTwoDoctorsAreOnVacation(newRequestDTO);
        }

        private bool CreateNewRequest(NewRequestDTO newRequestDTO)
        {
            VacationRequest vacationRequest = new VacationRequest(newRequestDTO);
            vacationRequestRepository.Insert(vacationRequest);
            return true;
        }

        private bool CreateRequestIfNoMoreThanTwoDoctorsAreOnVacation(NewRequestDTO newRequestDTO)
        {
            List<VacationRequest> vacationRequestList = vacationRequestRepository.GetVacationRequestsBySpecializationInDateInterval(newRequestDTO.Doctor,newRequestDTO.DateInterval);

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

        private bool CheckIfDoctorAlreadyMadeRequestForGivenDateInterval(Doctor doctor, DateInterval dateInterval)
        {
            
            return vacationRequestRepository.GetVacationRequestsByDoctorInDateInterval(doctor, dateInterval).Any();

        }
        
        public List<VacationRequest> GetVacationRequests()
        {
            return vacationRequestRepository.GetAll();
        }
        
        public void Accept(VacationRequest vacationRequest)
        {
            vacationRequestRepository.Accept(vacationRequest);
        }
        
        public void Reject(VacationRequest vacationRequest)
        {
            vacationRequestRepository.Reject(vacationRequest);
        }
    }
}
