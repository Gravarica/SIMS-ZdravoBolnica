using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.View.WardenForms.ViewModels;

namespace HospitalProject.Model
{
    public class Equipement : ViewModelBase
    {
        private int id;
        private int quantity;
        private string name;
        private EquipementType equipementType;
        private List<Allergies> alergens;
        private List<Equipement> replacements;




        public List<Allergies> Alergens
        {
            get
            {
                if (alergens == null)
                {
                    alergens = new System.Collections.Generic.List<Allergies>();
                }
                return alergens;
            }
            set
            {
                if (alergens != null)
                {
                    alergens.Clear();
                }
                if (value != null)
                {
                    foreach (Allergies al in value)
                    {
                        AddAlergen(al);
                    }
                }
                OnPropertyChanged(nameof(Alergens));
              
            }
        }
        
        public List<Equipement> Replacements
        {
            get
            {
                if (replacements == null)
                {
                    replacements = new System.Collections.Generic.List<Equipement>();
                }
                return replacements;
            }
            set
            {
                if (replacements != null)
                {
                    replacements.Clear();
                }
                if (value != null)
                {
                    foreach (Equipement al in value)
                    {
                        AddReplacement(al);
                    }
                }
                OnPropertyChanged(nameof(Replacements));
              
            }
        }
        
        public void AddAlergen(Allergies al)
        {
            if (al == null)
            {
                return;
            }

            if (alergens == null)
            {
                alergens = new System.Collections.Generic.List<Allergies>();
            }

            if (!alergens.Contains(al))
            {
                alergens.Add(al);
            }
           
        }
        
        public void AddReplacement(Equipement medicine)
        {
            if (medicine == null)
            {
                return;
            }

            if (replacements == null)
            {
                replacements = new System.Collections.Generic.List<Equipement>();
            }

            if (!replacements.Contains(medicine))
            {
                replacements.Add(medicine);
            }
           
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public EquipementType EquipementType
        {
            get
            {
                return equipementType;
            }
            set
            {
                equipementType = value;
                OnPropertyChanged(nameof(EquipementType));
            }
        }

        public Equipement(int id, int quantity, string name, EquipementType equipementType)
        {
            Id = id;
            Quantity = quantity;
            Name = name;
            EquipementType = equipementType;
        }

        public Equipement(int id, int quantity, string name, EquipementType equipementType, List<Allergies> alergens)
        {
            this.id = id;
            this.quantity = quantity;
            this.name = name;
            this.equipementType = equipementType;
            this.alergens = alergens;
        }

        public Equipement(int id, int quantity, string name, EquipementType equipementType, List<Equipement> replacements)
        {
            this.id = id;
            this.quantity = quantity;
            this.name = name;
            this.equipementType = equipementType;
            this.replacements = replacements;
        }

        public Equipement(int id, int quantity, string name, EquipementType equipementType, List<Allergies> alergens, List<Equipement> replacements)
        {
            this.id = id;
            this.quantity = quantity;
            this.name = name;
            this.equipementType = equipementType;
            this.alergens = alergens;
            this.replacements = replacements;
        }

        public Equipement(int quantity, string name, EquipementType equipementType, List<Allergies> alergens)
        {
            this.quantity = quantity;
            this.name = name;
            this.equipementType = equipementType;
            this.alergens = alergens;
        }

        public Equipement( int quantity, string name, EquipementType equipementType)
        {
            Quantity = quantity;
            Name = name;
            EquipementType = equipementType;
        }

        public Equipement(int id, int quantity)
        {
            this.id = id;
            this.quantity = quantity;
        }

        public Equipement(int id)
        {
            this.id = id;
        }

        public Equipement() { }

        public Equipement(EquipmentCheckBoxModel eq)
        {
            Id = eq.Id;
            Quantity = eq.Quantity;
            Name = eq.Name;
            EquipementType = eq.EquipementType;
            Alergens = eq.Alergens;
            Replacements = eq.Replacements;
            
        }

        public Equipement(int quantity, string name, EquipementType equipementType, List<Allergies> alergens, List<Equipement> replacements)
        {
            this.quantity = quantity;
            this.name = name;
            this.equipementType = equipementType;
            this.alergens = alergens;
            this.replacements = replacements;
        }
    }


}
