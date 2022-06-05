using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.View.WardenForms.ViewModels;

namespace HospitalProject.Model;

public class Allergies  : ViewModelBase
{
    
        private int _id;
        private String _name;
       
       
        public Allergies(int id, string name)
        {
            Name = name;
            Id = id;
        }
        public Allergies(string name)
        {
            Name = name;
        }

        public Allergies(int id)
        {
            this._id = id;
        }

        public Allergies(AddingMedicineAlergiesViewModel amav)
        {
            Name = amav.Name;
            Id = amav.Id;
        }

        public bool Matches(Allergies compareAllergies)
        {
            return Id == compareAllergies.Id;
        }

        public bool Matches(int id)
        {
            return Id == id;
        }
        public void UpdateAllergy(Allergies updateAllergy)
        {
            Name = updateAllergy.Name;
        }
        
        public int Id
        {
             get{ return _id; }
             set{ _id = value;
                  OnPropertyChanged(nameof(Id)); }
        }
        
        public string Name
        {
              get { return _name; }
              set{  _name = value;
                    OnPropertyChanged(nameof(Name)); }
        }

}
