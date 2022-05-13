using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Service
{
    public class DoctorService
    {
        private DoctorRepository _doctorRepository;
        private RoomService _roomService;

        public DoctorService(DoctorRepository doctorRepository, RoomService roomService)
        {
            _doctorRepository = doctorRepository;
            _roomService = roomService;
        }

        public IEnumerable<Doctor> getAll()
        {
            List<Doctor> doctors = _doctorRepository.GetAll().ToList();
            BindDoctorsForOrdinations(doctors);
            return doctors;
        }

        public Doctor GetById(int id)
        {
            Doctor doc = _doctorRepository.GetById(id);
            SetRoomForDoctor(doc);
            return doc;
        }

        public Doctor GetLoggedDoctor(string username)
        {
            return _doctorRepository.GetLoggedDoctor(username);   
        }

        public List<Doctor> GetDoctorsBySpecialization(Specialization specialization)
        {
            List<Doctor> doctors = _doctorRepository.GetDoctorsBySpecialization(specialization);
            BindDoctorsForOrdinations(doctors);
            return doctors;
        }

        private void BindDoctorsForOrdinations(List<Doctor> doctors)
        {
            doctors.ForEach(doctor => SetRoomForDoctor(doctor));
        }

        private void SetRoomForDoctor(Doctor doctor)
        {
            doctor.Ordination = _roomService.Get(doctor.Ordination.Id);
        }
    }
}
