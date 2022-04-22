using HospitalProject.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class DoctorController
    {

        private DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctorService.getAll();
        }

        public Doctor Get(int id)
        {
            return _doctorService.get(id);
        }

    }
}
