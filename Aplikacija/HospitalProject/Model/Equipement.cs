using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Model
{
    public class Equipement : ViewModelBase
    {
        private int id;
        private int quantity;
        private string name;
        private EquipementType equipementType;
        private List<Allergies> alergens;




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
    }


}
