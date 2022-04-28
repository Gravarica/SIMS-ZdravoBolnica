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
    }


}
