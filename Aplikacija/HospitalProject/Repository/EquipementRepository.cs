using HospitalProject.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.Model;

namespace HospitalProject.Repository
{
    public class EquipementRepository
    {
        private EquipementFileHandler _equipementFileHandler;
        private AllergiesRepository allergiesRepository;
        private int _equipementMaxId;
        private List<Equipement> _equipements = new List<Equipement>();

        public EquipementRepository(EquipementFileHandler equipementFileHandler, AllergiesRepository allergiesRepository)
        {
            this.allergiesRepository = allergiesRepository;
            _equipementFileHandler = equipementFileHandler;
            _equipements = _equipementFileHandler.ReadAll().ToList();
            BindAllergensForMedicine();
            _equipementMaxId = GetMaxId(_equipements);
        }

        private int GetMaxId(IEnumerable<Equipement> equipements)
        {
            return equipements.Count() == 0 ? 0 : equipements.Max(equipement => equipement.Id);
        }

        public Equipement Insert(Equipement equipement)
        {
            equipement.Id = ++_equipementMaxId;
            _equipementFileHandler.AppendLineToFile(equipement);
            _equipements.Add(equipement);
            return equipement;
        }

        public Equipement GetById(int id)
        {
            return _equipements.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Equipement> GetAll()
        {
            return _equipements;
        }

        public IEnumerable<Equipement> GetByType(EquipementType equipementType)
        {
            IEnumerable<Equipement> equipements = new List<Equipement>();
            foreach (Equipement equipement in _equipements)
            {
                if (equipement.EquipementType == equipementType)
                {
                    equipements.Append(equipement);
                }
            }

            return equipements;
        }

        public void Delete(int id)
        {
            Equipement removeEquipement = GetById(id);
            _equipements.Remove(removeEquipement);
            _equipementFileHandler.Save(_equipements);
        }

        public void Update(Equipement equipement)
        {
            Equipement updatedEquipement = GetById(equipement.Id);

            updatedEquipement.Quantity = equipement.Quantity;
            updatedEquipement.Name = equipement.Name;
            updatedEquipement.EquipementType = equipement.EquipementType;

            _equipementFileHandler.Save(_equipements);
        }

        public List<Equipement> GetAllMedicine()
        {
            return _equipements.Where(equipment => equipment.EquipementType == EquipementType.MEDICINE).ToList();
        }

        private void BindAllergensForMedicine()
        {
            foreach(Equipement equipement in _equipements)
            {
                if(equipement.EquipementType == EquipementType.MEDICINE)
                {
                    SetAllergiesForMedicine(equipement);
                }
            }
        }

        private void SetAllergiesForMedicine(Equipement medicine)
        {
            foreach(Allergies allergy in medicine.Alergens)
            {
                SetAllergen(allergy);
            }
        }

        private void SetAllergen(Allergies allergy)
        {
            allergy.Name = allergiesRepository.GetById(allergy.Id).Name;
        }
    }
}
