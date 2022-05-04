using System;
using Model;

namespace HospitalProject.Model
{
    public class EquipmentRelocation: ViewModelBase
    {
        private int _id;
        private Room _sourceRoom;
        private Room _destinationRoom;
        private Equipement _equipement;
        private int _quantity;
        private DateOnly _date;

        public EquipmentRelocation(Room sourceRoom, Room destinationRoom, Equipement equipement, int quantity, DateOnly date)
        {
            _sourceRoom = sourceRoom;
            _destinationRoom = destinationRoom;
            _equipement = equipement;
            _quantity = quantity;
            _date = date;
        }

        public EquipmentRelocation(int id, Room sourceRoom, Room destinationRoom, Equipement equipement, int quantity, DateOnly date)
        {
            _id = id;
            _sourceRoom = sourceRoom;
            _destinationRoom = destinationRoom;
            _equipement = equipement;
            _quantity = quantity;
            _date = date;
        }
    
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
            
        }
        
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                this._quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
            
        }
        
        public DateOnly Date
        {
            get
            {
                return _date;
            }
            set
            {
                this._date = value;
                OnPropertyChanged(nameof(Date));
            }
            
        }
        
        public Room SourceRoom
        {
            get
            {
                return _sourceRoom;
            }
            set
            {
                this._sourceRoom = value;
                OnPropertyChanged(nameof(SourceRoom));
            }
            
        }
        
        public Room DestinationRoom
        {
            get
            {
                return _destinationRoom;
            }
            set
            {
                this._destinationRoom = value;
                OnPropertyChanged(nameof(DestinationRoom));
            }
            
        }
        
        public Equipement Equipement
        {
            get
            {
                return _equipement;
            }
            set
            {
                this._equipement = value;
                OnPropertyChanged(nameof(Equipement));
            }
            
        }
    }
}

