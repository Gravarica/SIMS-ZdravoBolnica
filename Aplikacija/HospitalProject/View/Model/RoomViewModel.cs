﻿using System.ComponentModel;
using Model;

namespace HospitalProject.View.Model
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        int _roomId;
        int _roomNumber;
        string _roomType;
        int _roomFloor;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int RoomId
        {
            get
            {
                return _roomId;
            }
            set
            {
                if (value != _roomId)
                {
                    _roomId = value;
                    OnPropertyChanged(nameof(RoomId));
                }
            }
        }
        
        public int RoomFloor
        {
            get
            {
                return _roomFloor;
            }
            set
            {
                if (value != _roomFloor)
                {
                    _roomFloor = value;
                    OnPropertyChanged(nameof(RoomFloor));
                }
            }
        }
        
        public int RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {
                if (value != _roomNumber)
                {
                    _roomNumber = value;
                    OnPropertyChanged(nameof(RoomNumber));
                }
            }
        }
        
        public string TypeRoom
        {
            get
            {
                return _roomType;
            }
            set
            {
                if (value != _roomType)
                {
                    _roomType = value;
                    OnPropertyChanged(nameof(TypeRoom));
                }
            }
        }

    }
}

