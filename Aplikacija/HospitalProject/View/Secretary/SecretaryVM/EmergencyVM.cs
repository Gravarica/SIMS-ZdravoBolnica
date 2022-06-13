using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.Secretary.SecretaryV;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class EmergencyVM: BaseViewModel
    {
        private int _id;
        private int _findjmbg;
        private Room _room;
        
        private Patient _selectedItem;
        private ExaminationType _selectedExamination;
        private Specialization _specialization;
      
        
        private RelayCommand _addGuest; 
        public RelayCommand SearchCommand { get; set; }
        private RelayCommand _createEmergency;

        public ObservableCollection<Patient> Patients { get; set; }

        private AppointmentController _appointmentController;
        private RoomControoler _roomController;
        
        private List<ComboBoxData<ExaminationType>> examinationTypeComboBox = new List<ComboBoxData<ExaminationType>>();
        private List<ComboBoxData<Room>> roomsComboBox = new List<ComboBoxData<Room>>();
        private List<ComboBoxData<Specialization>> specializationComboBox = new List<ComboBoxData<Specialization>>();
        
        

        public EmergencyVM()
        {
            var app = System.Windows.Application.Current as App; 
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll().ToList());
            AllPatients = new ObservableCollection<Patient>(app.PatientController.GetAll().ToList());

            InitializeControllers();
            FillComboData();
        }

        public ObservableCollection<Patient> AllPatients { get; set; }

        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;
            _appointmentController = app.AppointmentController;
            _roomController = app.RoomController;
            SearchCommand = new RelayCommand(o => ExecuteSearchQuantityCommand(), o => true);

        }

        public Patient SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public int FindJmbg
        {
            get
            {
                return _findjmbg;
            }
            set
            {
                _findjmbg = value;
                OnPropertyChanged(nameof(FindJmbg));
            }
        }

        public RelayCommand AddGuest
        {
            get
            {
                return _addGuest ?? (_addGuest = new RelayCommand(param => ExecuteAddGuestCommand(),
                                                                                     param => CanExecute()));
            }
        }


        public RelayCommand CreateEmergency
        {
            get
            {
                return _createEmergency ?? (_createEmergency = new RelayCommand(param => ExecuteCreateEmergencyCommand(),
                                                                                     param => CanExecuteCreateEmergency()));
            }
        }
    
        private bool CanExecuteCreateEmergency()
        {
            return SelectedItem != null && SelectedRoom != null ;
        }
        
        private bool CanExecute()
        {
            return true;
        }
        
        private void ExecuteAddGuestCommand()
        {
            AddGuestPatient view = new AddGuestPatient();
            AddGuestVM viewModel = new AddGuestVM(Patients);
            view.DataContext = viewModel;
            SecretaryMainViewVM.Instance.CurrentView = view;
        }

    
        private void ExecuteCreateEmergencyCommand() 
        {

           // List<Appointment> availableAppointments =  appointmentController.FirstAvailableWithoutRescheduling(SelectedSpecialization, SelectedItem, SelectedExamination, SelectedRoom);
        //   if (availableAppointments.Count == 0)
            {
              //  List<AppointmentsDTO> moveAppointments = appointmentController.BestOptionsForRescheduling(SelectedSpecialization);
                //RescheduleAppV view = new RescheduleAppV();
              //  view.DataContext = new MoveAppVM(moveAppointments, SelectedSpecialization, SelectedItem, SelectedExamination, SelectedRoom);
             //   view.ShowDialog();
            }


           // else 
            {
                //DateTime date, int duration, Doctor doctor, Patient patient, Room room, ExaminationType examinationType
              //  Appointment firstAvailable = availableAppointments.First();
              //  appointmentController.Create(new Appointment(firstAvailable.Date, 30, firstAvailable.Doctor, SelectedItem, SelectedRoom, SelectedExamination));

            }

        }

        private void FillComboData()
        {
           FillSpecializationComboData();
           FillExaminationTypeComboData();
           FillRoomComboData();
        }

       private void FillSpecializationComboData()
         {
              foreach (Specialization specialization in Enum.GetValues(typeof(Specialization)))
              {
                      specializationComboBox.Add(new ComboBoxData<Specialization>
                      { Name = specialization.ToString(), Value = specialization });
              }
          }

        private void FillExaminationTypeComboData()
        {
            foreach (ExaminationType examType in Enum.GetValues(typeof(ExaminationType)))
            {
                    examinationTypeComboBox.Add(new ComboBoxData<ExaminationType>
                    { Name = examType.ToString(), Value = examType });
            }
        }

        private void FillRoomComboData()
        {
            foreach (Room room in _roomController.GetAll())
            {
                if (room.RoomType != RoomType.stockroom)
                {
                        roomsComboBox.Add(new ComboBoxData<Room>
                        { Name = room.Number.ToString(), Value = room });
                }
            }
        }

        public List<ComboBoxData<ExaminationType>> ExaminationTypeComboBox
        {
            get
            {
                return examinationTypeComboBox;
            }
            set
            {
                examinationTypeComboBox = value;
                OnPropertyChanged(nameof(ExaminationTypeComboBox));
            }
        }

        public List<ComboBoxData<Room>> RoomTypeComboBox
        {
            get
            {
                return roomsComboBox;
            }
            set
            {
                roomsComboBox = value;
                OnPropertyChanged(nameof(RoomTypeComboBox));
            }
        }

        public List<ComboBoxData<Specialization>> SpecializationComboBox
        {
            get
            {
                return specializationComboBox;
            }
            set
            {
                specializationComboBox = value;
                OnPropertyChanged(nameof(SpecializationComboBox));
            }
        }
        
        public ExaminationType SelectedExamination
        {
            get
            {
                return _selectedExamination;
            }
            set
            {
                _selectedExamination = value;
                OnPropertyChanged(nameof(SelectedExamination));
            }
        }

        public Specialization SelectedSpecialization
        {
            get
            {
                return _specialization;
            }
            set
            {
                _specialization = value;
                OnPropertyChanged(nameof(SelectedSpecialization));
            }
        }

        public Room SelectedRoom
        {
            get
            {
                return _room;
            }
            set
            {
                _room = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        private void ExecuteSearchQuantityCommand()
        {
            if ( FindJmbg==0)
            {
                MessageBox.Show("Jmbg must be integer number!", "warning", MessageBoxButton.OK);
            }
            else
            {
                Patients.Clear();
                foreach (Patient patient in AllPatients)
                {
                    if (FindJmbg == patient.Jmbg)
                    {
                        Patients.Add(patient);
                    }


                }

                if (string.IsNullOrEmpty(FindJmbg.ToString())
                    == true)
                {
                    Patients = AllPatients;
                }
            }
        }
    }
}
