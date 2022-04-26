﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using HospitalProject.Core;
using HospitalProject.View.Model;

namespace HospitalProject.View.WardenForms
{
    public class MainViewModel : BaseViewModel
    {
        private object _momentalView { get; set; }
        public WardenRoomControl WardenRoomControl { get; set; }
        public RelayCommand RoomViewCommand { get; set; }
        
        public WardenEquipementView WardenEquipementView { get; set; }
        public RelayCommand EquipementCommand { get; set; }
        
        public WardenEquipemntRelocationControl WardenEquipemntRelocationControl { get; set; }
        
        public RelayCommand EquipementRelocationCommand { get; set; }
        
        
        

        public object MomentalView
        {
            get => _momentalView;
            set
            {
                _momentalView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            WardenRoomControl = new WardenRoomControl();
            WardenEquipementView = new WardenEquipementView();
            WardenEquipemntRelocationControl = new WardenEquipemntRelocationControl();

            MomentalView = WardenRoomControl;
             RoomViewCommand = new RelayCommand(o =>
                 {
                     MomentalView = WardenRoomControl;
                 }
                 );
             EquipementCommand = new RelayCommand(o =>
                 {
                     MomentalView = WardenEquipementView;
                 }
             );
             EquipementRelocationCommand = new RelayCommand(o
                 =>
             {
                 MomentalView = WardenEquipemntRelocationControl;
             });
        }
        
        
    }
}

