using HospitalProject.DataTransferObjects;
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

        public bool Create(NewRequestDTO newRequestDTO)
        {
            return vacationRequestService.Create(newRequestDTO);
        }

        public List<VacationRequest> GetVacationRequestsForDoctor(Doctor doctor)
        {
            return vacationRequestService.GetVacationRequestsForDoctor(doctor);
        }
        
        public List<VacationRequest> GetVacationRequests()
        {
            return vacationRequestService.GetVacationRequests();
        }
        
        public void Accept(VacationRequest vacationRequest)
        {
            vacationRequestService.Accept(vacationRequest);
        }
        
        public void Reject(VacationRequest vacationRequest)
        {
            vacationRequestService.Reject(vacationRequest);
        }
    }
}
