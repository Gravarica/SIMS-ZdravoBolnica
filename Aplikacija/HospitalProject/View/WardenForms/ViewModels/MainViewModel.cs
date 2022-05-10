using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.WardenForms.Views;

namespace HospitalProject.View.WardenForms.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private EquipmentRelocationController _equipmentRelocationController;
        private Thread thread;
        private object _momentalView { get; set; }
        public WardenRoomControl WardenRoomControl { get; set; }
        public RelayCommand RoomViewCommand { get; set; }
        
        public EquipementViewModel WardenEquipementView { get; set; }
        public RelayCommand EquipementCommand { get; set; }

        public WardenEquipemntRelocationViewModel WardenEquipemntRelocationViewModel { get; set; }
        
        public RelayCommand EquipementRelocationCommand { get; set; }
        
        public RoomRenovationViewModel RoomRenovationViewModel { get; set; }
        
        public RelayCommand RoomRenovationCommand { get; set; }
        
        
        public MedicineViewModel MedicineViewModel { get; set; }
        public RelayCommand MedicineViewCommand { get; set; }






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
            var app = System.Windows.Application.Current as App;
            _equipmentRelocationController = app.EquipmentRelocationController;
            instance = this;
            WardenRoomControl = new WardenRoomControl();
            WardenEquipementView = new EquipementViewModel();
            WardenEquipemntRelocationViewModel = new WardenEquipemntRelocationViewModel();
            RoomRenovationViewModel = new RoomRenovationViewModel();
            MedicineViewModel = new MedicineViewModel();

            MomentalView = WardenRoomControl;
            
            MedicineViewCommand =new RelayCommand(o =>
                {
                    MomentalView = MedicineViewModel;
                }
            );
            
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

             ThreadStart threadStart = new ThreadStart(StartRelocationThread);
             thread = new Thread(threadStart);
             thread.Start();

        }

        public void StartRelocationThread()
        {
            while (true)
            {
                List<EquipmentRelocation> todaysRelocations = _equipmentRelocationController.ExecuteRelocations();
                if (todaysRelocations.Count != 0)
                {
                    string message = String.Empty;
                    bool hasMessage = false;
                    foreach (EquipmentRelocation er in todaysRelocations)
                    {
                        hasMessage = true;
                        _equipmentRelocationController.Delete(er.Id);
                        message += (er.Id.ToString()+",");
                    }
                    if(hasMessage){message = message.Remove(message.Length - 1,1);}
                    MessageBox.Show("Sucsefful relocations ids:" + message, "Todayse relocations");
                }
                Thread.Sleep(60*1000);
                
            }
            
        }
        
        
    }
}

