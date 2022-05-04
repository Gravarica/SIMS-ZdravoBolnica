using System.Collections.Generic;
using HospitalProject.Model;
using HospitalProject.Service;

namespace HospitalProject.Controller
{
    public class EquipmentRelocationController
    {
        private EquipmentRelocationService _relocationService;

        public EquipmentRelocationController(EquipmentRelocationService relocationService)
        {
            _relocationService = relocationService;
        }

        public EquipmentRelocation Create(EquipmentRelocation relocation)
        {
            return _relocationService.Create(relocation);
        }

        public List<EquipmentRelocation> ExecuteRelocations()
        {
           return _relocationService.ExecuteRelocations();
        }

        public void Delete(int id)
        {
            _relocationService.Delete(id);
        }
    }
}

