using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.Model;
using HospitalProject.Repository;

namespace HospitalProject.Service
{
    public class EquipementService
    {
        private EquipementRepository equipementRepository;

        public EquipementService(EquipementRepository equipementRepository)
        {
            this.equipementRepository = equipementRepository;
        }

        public Equipement Create(Equipement equipement)
        {
            return equipementRepository.Insert(equipement);
        }

        public IEnumerable<Equipement> getAll()
        {
            var equipements = equipementRepository.GetAll();
            return equipements;
        }

       

        public Equipement GetById(int id)
        {
            return this.equipementRepository.GetById(id);
        }

        public void Update(Equipement equipement)
        {
            equipementRepository.Update(equipement);
        }

        public void Delete(int id)
        {
            equipementRepository.Delete(id);
        }

        public List<Equipement> GetAllMedicine()
        {
            return equipementRepository.GetAllMedicine();
        }
    }
}
