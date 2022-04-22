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

        public DoctorService(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<Doctor> getAll()
        {
            return _doctorRepository.GetAll();
        }

        public Doctor get(int id)
        {
            return _doctorRepository.Get(id);
        }
    }
}
