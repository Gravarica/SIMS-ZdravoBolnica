﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private RelayCommand _addGuest;
        private RelayCommand _search;
        private RelayCommand _createEmergency;

        private Patient _selectedItem;
        public ObservableCollection<Patient> Patients { get; set; }
        PatientController _patientController;

        private AppointmentController appointmentController;
        private RoomControoler roomController;
        private List<ComboBoxData<ExaminationType>> examinationTypeComboBox = new List<ComboBoxData<ExaminationType>>();
        private List<ComboBoxData<Room>> roomsComboBox = new List<ComboBoxData<Room>>();
        private List<ComboBoxData<Specialization>> specializationComboBox = new List<ComboBoxData<Specialization>>();
        
        //SpecializationComboBox
        //FindJmbg
        //Search

        private int id;
        private int findjmbg;


        public EmergencyVM()
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;
            appointmentController = app.AppointmentController;
            roomController = app.RoomController;
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll().ToList());
           
           FillComboData();
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
                return findjmbg;
            }
            set
            {
                findjmbg = value;
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

        public RelayCommand Search
        {
            get
            {
                return _search ?? (_search = new RelayCommand(param => ExecuteSearchCommand(),
                                                                                     param => CanExecuteSearch()));
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
        private bool CanExecute()
        {
            return true;
        }
        private bool CanExecuteSearch()
        {
            //if (String.IsNullOrEmpty(Convert.ToString(FindJmbg))) { return false; }
            return true; 
            
        }

        private bool CanExecuteCreateEmergency()
        {
            return SelectedItem != null && SelectedRoom != null ;

        }
        private void ExecuteAddGuestCommand()
        {
            var app = System.Windows.Application.Current as App;
 
            AddGuestPatient view = new AddGuestPatient();

            AddGuestVM viewModel = new AddGuestVM(view);
           
            view.DataContext = viewModel;
            view.ShowDialog();
            if (viewModel.ModalResult == true)
            {
                Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            }
        }

        private void ExecuteSearchCommand()
        {

          //  Patient p = _patientController.GetByJmbg(FindJmbg);
            Patients.Clear();
           // Patients.Add(p);

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
                  specializationComboBox.Add(new ComboBoxData<Specialization> { Name = specialization.ToString(), Value = specialization });
              }
          }

        private void FillExaminationTypeComboData()
        {
            foreach (ExaminationType examType in Enum.GetValues(typeof(ExaminationType)))
            {
                examinationTypeComboBox.Add(new ComboBoxData<ExaminationType> { Name = examType.ToString(), Value = examType });
            }
        }

        private void FillRoomComboData()
        {
            foreach (Room room in roomController.GetAll())
            {
                if (room.RoomType != RoomType.stockroom)
                {
                    roomsComboBox.Add(new ComboBoxData<Room> { Name = room.Number.ToString(), Value = room });
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
        private ExaminationType selectedExamination;
        private Room room;
        private Specialization specialization;
        public ExaminationType SelectedExamination
        {
            get
            {
                return selectedExamination;
            }
            set
            {
                selectedExamination = value;
                OnPropertyChanged(nameof(SelectedExamination));
            }
        }

        public Specialization SelectedSpecialization
        {
            get
            {
                return specialization;
            }
            set
            {
                specialization = value;
                OnPropertyChanged(nameof(SelectedSpecialization));
            }
        }

        public Room SelectedRoom
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

    }
}
