using System.Collections.Generic;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;

namespace HospitalProject.View.WardenForms.ViewModels
{
    public class RoomCheckBoxModel :BaseViewModel
    {
        private int id;
        private int number;
        private int floor;
        private string roomType;
        private System.Collections.Generic.List<Equipement> equipment;
        private bool isChecked;
        private string name;
        
        
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
        
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        
        public int Floor
        {
            get
            {
                return floor;
            }
            set
            {
                floor = value;
                OnPropertyChanged(nameof(Floor));
            }
        }
        
        public string RoomType
        {
            get
            {
                return roomType;
            }
            set
            {
                roomType = value;
                OnPropertyChanged(nameof(RoomType));
            }
        }
        
        public List<Equipement> Equipement
        {
            get
            {
                if (equipment == null)
                {
                    equipment = new System.Collections.Generic.List<Equipement>();
                }
                return equipment;
            }
            set
            {
                if (equipment != null)
                {
                    equipment.Clear();
                }
                if (value != null)
                {
                    foreach (Equipement eq in value)
                    {
                        AddEquipment(eq);
                    }
                }
              
            }
        }
        
        public void AddEquipment(Equipement eq)
        {
            if (eq == null)
            {
                return;
            }

            if (equipment == null)
            {
                equipment = new System.Collections.Generic.List<Equipement>();
            }

            if (!equipment.Contains(eq))
            {
                equipment.Add(eq);
            }
           
        }

        public RoomCheckBoxModel(int id, int number, int floor, string roomType, List<Equipement> equipment)
        {
            this.id = id;
            this.number = number;
            this.floor = floor;
            this.roomType = roomType;
            this.equipment = equipment;
        }

        public RoomCheckBoxModel(int number, int floor, string roomType, List<Equipement> equipment)
        {
            this.number = number;
            this.floor = floor;
            this.roomType = roomType;
            this.equipment = equipment;
            this.isChecked = false;
        }

        
        
        public RoomCheckBoxModel(Room room)
        {
            Id = room._id;
            Floor = room._floor;
            RoomType = room._roomType.ToString();
            Equipement = room.Equipment;
            IsChecked = false;
        }

        public RoomCheckBoxModel()
        {
        }

        public RoomCheckBoxModel(string name)
        {
            this.name = name;
        }
    }
}

