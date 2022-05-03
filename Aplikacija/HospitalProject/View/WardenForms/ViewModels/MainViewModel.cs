﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using HospitalProject.Core;
using HospitalProject.View.Model;
using HospitalProject.View.WardenForms.ViewModels;

namespace HospitalProject.View.WardenForms
{
    public class MainViewModel : BaseViewModel
    {
        private object _momentalView { get; set; }
        public WardenRoomControl WardenRoomControl { get; set; }
        public RelayCommand RoomViewCommand { get; set; }
        
        public EquipementViewModel WardenEquipementView { get; set; }
        public RelayCommand EquipementCommand { get; set; }

        public WardenEquipemntRelocationViewModel WardenEquipemntRelocationViewModel { get; set; }
        
        public RelayCommand EquipementRelocationCommand { get; set; }
        
        public RoomRenovationViewModel RoomRenovationViewModel { get; set; }
        
        public RelayCommand RoomRenovationCommand { get; set; }
        
        
        
        
        

        public object MomentalView
        {
            get => _momentalView;
            set
            {
                _momentalView = value;
                OnPropertyChanged();
            }
        }

        private static MainViewModel instance;
        public static MainViewModel Instance => instance;
        public MainViewModel()
        {
            instance = this;
            WardenRoomControl = new WardenRoomControl();
            WardenEquipementView = new EquipementViewModel();
            WardenEquipemntRelocationViewModel = new WardenEquipemntRelocationViewModel();
            RoomRenovationViewModel = new RoomRenovationViewModel();

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
                 MomentalView = WardenEquipemntRelocationViewModel;
             });
             RoomRenovationCommand = new RelayCommand(o => { MomentalView = RoomRenovationViewModel; });

        }
        
        
    }
}

