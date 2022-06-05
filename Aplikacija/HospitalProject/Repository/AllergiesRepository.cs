using System;
using System.Collections.Generic;
using System.Linq;
using HospitalProject.FileHandler;
using HospitalProject.Model;
using Model;
using Repository;

namespace HospitalProject.Repository;

public class AllergiesRepository
{
      
        private List<Allergies> _allergies;
        private AllergiesFileHandler _allergiesFileHandler;
        private int _allergiesMaxId;

        public AllergiesRepository(AllergiesFileHandler allergiesFileHandler)
        {
            _allergiesFileHandler = allergiesFileHandler;
            _allergies = _allergiesFileHandler.ReadAll().ToList();
            _allergiesMaxId = GetMaxId();

        }
        private int GetMaxId() {
            return _allergies.Count() == 0 ? 0 : _allergies.Max(Allergy => Allergy.Id);
        }
        
        public void Insert(Allergies allergy)
        {
            allergy.Id = ++_allergiesMaxId;
            _allergies.ToList().Add(allergy);
            _allergiesFileHandler.AppendLineToFile(allergy);
        }

        public Allergies GetById(int id)
        {
            return _allergies.FirstOrDefault(allergy => allergy.Matches(id));
        }

        public List<Allergies> GetAll()
        {
         return _allergies;
        }

        public void Update(Allergies updateAllergen)
        {
            _allergies.ForEach(allergy => UpdateNameIfMatches(allergy, updateAllergen));
        }

        private void UpdateNameIfMatches(Allergies allergy, Allergies updateAllergen)
        {
            if (!allergy.Matches(updateAllergen)) return;
            allergy.UpdateAllergy(updateAllergen);
            _allergiesFileHandler.Save(_allergies);
        }

        public void Delete(int id)
        {
            _allergies.ForEach(allergy => RemoveAllergenIfMatches(allergy, id));
        }

        private void RemoveAllergenIfMatches(Allergies allergy, int id)
        {
            if (!allergy.Matches(id)) return;
            _allergies.Remove(allergy);
            _allergiesFileHandler.Save(_allergies);
        }

}
