using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.Model;
using HospitalProject.Service;

namespace HospitalProject.Controller
{
    public class EquipementController
    {
        private EquipementService _equipementService;

        public EquipementController(EquipementService equipementService)
        {
            _equipementService = equipementService;
        }

        public Equipement Create(Equipement equipement)
        {
            return _equipementService.Create(equipement);
        }

        public IEnumerable<Equipement> GetAll()
        {
            return _equipementService.getAll();
        }

        public void Update(Equipement equipement)
        {
            _equipementService.Update(equipement);
        }

        public void Delete(int id)
        {
            _equipementService.Delete(id);
        }
    }
}
