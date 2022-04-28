using HospitalProject.FileHandler;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Repository
{
    public class PrescriptionRepository
    {

        private PrescriptionFileHandler prescriptionFileHandler;

        private IEnumerable<Prescription> prescriptions;

        public PrescriptionRepository(PrescriptionFileHandler prescriptionFileHandler)
        {
            this.prescriptionFileHandler = prescriptionFileHandler;
            prescriptions = prescriptionFileHandler.ReadAll();
        }

        public IEnumerable<Prescription> GetAll()
        {
            return prescriptions;
        }
        
    }
}
