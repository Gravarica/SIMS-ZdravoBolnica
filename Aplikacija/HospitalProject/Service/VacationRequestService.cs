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

        public bool Create(DateTime submissionDate, Doctor doctor, DateTime startDate, DateTime endDate, string description, bool isUrgent, RequestState requestState)
        {
            List<VacationRequest> vacationRequestList = vacationRequestRepository.GetVacationRequestsBySpecializationInDateInterval(doctor.Specialization, startDate, endDate);

            if (VacationRequestValidation.CanCreateNewVacationRequest(vacationRequestList))
            {
                VacationRequest vacationRequest = new VacationRequest(submissionDate,doctor,startDate,endDate,description,isUrgent,requestState);
                return true;
            } 
            else
            {
                return false;
            }
        }

        public List<VacationRequest> GetVacationRequestsForDoctor(Doctor doctor)
        {
            return vacationRequestRepository.GetRequestsForDoctor(doctor);
        }
    }
}
