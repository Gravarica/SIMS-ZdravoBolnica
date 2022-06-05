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

        private IHandleData<VacationRequest> vacationRequestFileHandler;
        private DoctorRepository doctorRepository;
        private List<VacationRequest> vacationRequests;
        private int vacationRequestMaxId;

        public VacationRequestRepository(DoctorRepository doctorRepository)
        {
            this.vacationRequestFileHandler = new VacationRequestFileHandler(FilePathStorage.VACATION_REQUEST_FILE);
            this.doctorRepository=doctorRepository;
            InstantiateVacationRequestList();
        }

        private int GetMaxId()
        {
            return !vacationRequests.Any() ? 0 : vacationRequests.Max(prescription => prescription.Id);
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
            vacationRequests.ForEach(SetDoctorForVacationRequest);
        }

        public List<VacationRequest> GetAll()
        {
            return vacationRequests;
        }

        public VacationRequest GetById(int id)
        {
            return vacationRequests.FirstOrDefault(x => x.Id == id);
        }

        public List<VacationRequest> GetVacationRequestsForDoctor(Doctor doctor)
        {
            return vacationRequests.Where(vacationRequest => vacationRequest.DoctorMatches(doctor)).ToList();
        }

        public List<VacationRequest> GetVacationRequestByDateInterval(DateInterval dateInterval)
        {
            return vacationRequests.Where(vacationRequest => vacationRequest.DateInterval.Overlaps(dateInterval)).ToList();
        }

        public void Insert(VacationRequest vacationRequest)
        {
            vacationRequest.Id = ++vacationRequestMaxId;
            vacationRequests.Add(vacationRequest);
            vacationRequestFileHandler.SaveOneEntity(vacationRequest);
        }

        public List<VacationRequest> GetVacationRequestByState(RequestState requestState)
        {
            return vacationRequests.Where(vacationRequest => vacationRequest.RequestStateMatches(requestState)).ToList();   
        }

        public List<VacationRequest> GetVacationRequestsBySpecialization(Specialization specialization)
        {
            return vacationRequests.Where(vacationRequest => vacationRequest.SpecializationMatches(specialization)).ToList();
        }

        public List<VacationRequest> GetVacationRequestsBySpecializationInDateInterval(Doctor doctor, DateInterval dateInterval)
        {
            return GetVacationRequestByDateInterval(dateInterval).Where(vacationRequest => vacationRequest.HasDistinctDoctorForSpecialization(doctor)).ToList();
        }

        public List<VacationRequest> GetVacationRequestsByDoctorInDateInterval(Doctor doctor, DateInterval dateInterval)
        {
            return GetVacationRequestByDateInterval(dateInterval).Where(vacationRequest => vacationRequest.DoctorMatches(doctor)).ToList();
        }
        
        public void Accept(VacationRequest vacationRequest)
        {
            VacationRequest updateVacationRequest = GetById(vacationRequest.Id);
            updateVacationRequest.RequestState = RequestState.APPROVED;
            updateVacationRequest.SecretaryDescription = vacationRequest.SecretaryDescription;
            vacationRequestFileHandler.Save(vacationRequests);
        }
        
        public void Reject(VacationRequest vacationRequest)
        {
            VacationRequest updateVacationRequest = GetById(vacationRequest.Id);
            updateVacationRequest.RequestState = RequestState.DENIED;
            updateVacationRequest.SecretaryDescription = vacationRequest.SecretaryDescription;
            vacationRequestFileHandler.Save(vacationRequests);

        }
    }
}
