using HospitalProject.FileHandler;
using HospitalProject.Model;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class VacationRequestRepository
    {

        private VacationRequestFileHandler vacationRequestFileHandler;
        private DoctorRepository doctorRepository;
        private List<VacationRequest> vacationRequests;
        private int vacationRequestMaxId;

        public VacationRequestRepository(VacationRequestFileHandler vacationRequestFileHandler, DoctorRepository doctorRepository)
        {
            this.vacationRequestFileHandler = vacationRequestFileHandler;
            this.doctorRepository=doctorRepository;
            InstantiateVacationRequestList();
        }

        private int GetMaxId()
        {
            return vacationRequests.Count() == 0 ? 0 : vacationRequests.Max(prescription => prescription.Id);
        }

        private void InstantiateVacationRequestList()
        {
            vacationRequests = vacationRequestFileHandler.ReadAll().ToList();
            BindDoctorsForVacationRequests();
            vacationRequestMaxId = GetMaxId();
        }

        private void SetDoctorForVacationRequest(VacationRequest vacationRequest)
        {
            vacationRequest.Doctor = doctorRepository.GetById(vacationRequest.Doctor.Id);
        }

        private void BindDoctorsForVacationRequests()
        {
            vacationRequests.ForEach(vacationRequest => SetDoctorForVacationRequest(vacationRequest));
        }

        public List<VacationRequest> GetAll()
        {
            return vacationRequests;
        }

        public VacationRequest GetById(int id)
        {
            return vacationRequests.FirstOrDefault(x => x.Id == id);
        }

        public List<VacationRequest> GetRequestsForDoctor(Doctor doctor)
        {
            return vacationRequests.Where(vacationRequest => vacationRequest.Doctor.Id == doctor.Id).ToList();
        }

        public List<VacationRequest> GetVacationRequestByDateInterval(DateTime startDate, DateTime endDate)
        {
            return vacationRequests.Where(vacationRequests => CheckIfVacationRequestIsInDateInterval(vacationRequests, startDate, endDate)).ToList();
        }

        public void Insert(VacationRequest vacationRequest)
        {
            vacationRequest.Id = ++vacationRequestMaxId;
            vacationRequests.Add(vacationRequest);
            vacationRequestFileHandler.AppendLineToFile(vacationRequest);
        }

        public List<VacationRequest> GetVacationRequestByState(RequestState requestState)
        {
            return vacationRequests.Where(vacationRequest => vacationRequest.RequestState == requestState).ToList();   
        }

        public List<VacationRequest> GetVacationRequestsBySpecialization(Specialization specialization)
        {
            return vacationRequests.Where(vacationRequest => vacationRequest.Doctor.Specialization == specialization).ToList();
        }

        public List<VacationRequest> GetVacationRequestsBySpecializationInDateInterval(Specialization specialization, DateTime startDate, DateTime endDate)
        {
            return GetVacationRequestByDateInterval(startDate,endDate).Where(vacationRequest => vacationRequest.Doctor.Specialization == specialization).ToList();
        }

        private bool CheckIfVacationRequestIsInDateInterval(VacationRequest vacationRequest, DateTime startDate, DateTime endDate)
        {
            if (IsDateBetweenTwoDates(vacationRequest.StartDate, startDate, endDate))
            {
                return true;
            } 
            else if (IsDateBetweenTwoDates(vacationRequest.EndDate, startDate, endDate))
            {
                return true;
            }
            else if (vacationRequest.StartDate <= startDate && vacationRequest.EndDate >= endDate)
            {
                return true;
            }

            return false;
        }

        private bool IsDateBetweenTwoDates(DateTime date, DateTime intervalStart, DateTime intervalEnd)
        {
            return date >= intervalStart && date <= intervalEnd;
        }
    }
}
