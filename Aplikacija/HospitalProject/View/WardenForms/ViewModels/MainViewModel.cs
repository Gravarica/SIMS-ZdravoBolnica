using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.WardenForms.Views;
using Syncfusion.Data.Extensions;

namespace HospitalProject.View.WardenForms.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private EquipmentRelocationController _equipmentRelocationController;
        private EquipementController _equipementController;
        private Thread thread;
        private object _momentalView { get; set; }
        public WardenRoomControl WardenRoomControl { get; set; }
        public RelayCommand RoomViewCommand { get; set; }
        
        public MedicineReportViewModel MedicineReportViewModel { get; set; }
        public RelayCommand MedicineReportCommand { get; set; }
        
        public EquipementViewModel WardenEquipementView { get; set; }
        public RelayCommand EquipementCommand { get; set; }

        public WardenEquipemntRelocationViewModel WardenEquipemntRelocationViewModel { get; set; }
        
        public RelayCommand EquipementRelocationCommand { get; set; }
        
        public RoomRenovationViewModel RoomRenovationViewModel { get; set; }
        
        public RelayCommand RoomRenovationCommand { get; set; }
        
        
        public MedicineViewModel MedicineViewModel { get; set; }
        public RelayCommand MedicineViewCommand { get; set; }
        
        public AddingMedicineView AddingMedicineView { get; set; }



        private ObservableCollection<Equipement> MedicineItems { get; set; }
        
        public RelayCommand GradesCommand { get; set; }
        public WardenGradesViewModel WardenGradesViewModel { get; set; }

        public Help HelpViewModel { get; set; }

        public RelayCommand HelpCommand { get; set; }



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

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _equipmentRelocationController = app.EquipmentRelocationController;
            _equipementController = app.EquipementController;
        }

        private void InitialiseItems()
        {
            MedicineItems = _equipementController.GetAll().ToObservableCollection();
        }

        private void InstantiateVievModels()
        {
            WardenRoomControl = new WardenRoomControl();
            WardenEquipementView = new EquipementViewModel();
            WardenEquipemntRelocationViewModel = new WardenEquipemntRelocationViewModel();
            RoomRenovationViewModel = new RoomRenovationViewModel();
            MedicineViewModel = new MedicineViewModel();
            AddingMedicineView = new AddingMedicineView();
            MedicineReportViewModel = new MedicineReportViewModel();
            WardenGradesViewModel = new WardenGradesViewModel();
            HelpViewModel = new Help();
        }

        private void InstaliseComamnds()
        {
            MedicineReportCommand =new RelayCommand(o =>
                {
                    MomentalView = MedicineReportViewModel;
                }
            );

            HelpCommand = new RelayCommand(o =>
            {
                MomentalView = HelpViewModel;
            }
           );

            MedicineViewCommand =new RelayCommand(o =>
                {
                    MomentalView = MedicineViewModel;
                }
            );
            
            GradesCommand =new RelayCommand(o =>
                {
                    MomentalView = WardenGradesViewModel;
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
        }

        private void initialiseThread()
        {
            ThreadStart threadStart = new ThreadStart(StartRelocationThread);
            thread = new Thread(threadStart);
            thread.Start();
        }
        
        public MainViewModel()
        {
            instance = this;
            InstantiateControllers();
            InitialiseItems();
            InstantiateVievModels();
            InstaliseComamnds();
            
            MomentalView = WardenRoomControl;

            initialiseThread();
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
                    MessageBox.Show("Sucsefful relocations ids:" + message, "Todays relocations");
                }
                Thread.Sleep(60*1000);
                
            }
            
        }
        
        
    }
}

