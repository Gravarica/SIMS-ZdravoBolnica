using HospitalProject.Model;
using HospitalProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Controller
{
    public class AnamnesisController
    {

        private AnamnesisService _anamnesisService;

        public AnamnesisController(AnamnesisService anamnesisService)
        {
            _anamnesisService=anamnesisService;
        }

        public void Create(Anamnesis anamnesis)
        {
            _anamnesisService.Create(anamnesis);
        }

        public void Update(Anamnesis anamnesis)
        {
            _anamnesisService.Update(anamnesis);
        }

        public IEnumerable<Anamnesis> GetAnamnesisByMedicalRecord(int patientId)
        {
            return _anamnesisService.GetAnamnesesByMedicalRecord(patientId);
        }

    }
}
