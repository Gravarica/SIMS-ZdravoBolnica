using HospitalProject.Model;
using HospitalProject.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class VacationRequestController
    {
        private VacationRequestService vacationRequestService;

        public VacationRequestController(VacationRequestService vacationRequestService)
        {
            this.vacationRequestService = vacationRequestService;
        }

        public bool Create(DateTime submissionDate, Doctor doctor, DateTime startDate, DateTime endDate, string description, bool isUrgent, RequestState requestState)
        {
            return vacationRequestService.Create(submissionDate, doctor, startDate, endDate, description, isUrgent, requestState);
        }

        public List<VacationRequest> GetVacationRequestsForDoctor(Doctor doctor)
        {
            return vacationRequestService.GetVacationRequestsForDoctor(doctor);
        }
    }
}
