using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.Util;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class EditAppVM : BaseViewModel
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private Patient _patient;
        private Doctor _doctor;
        
        private Appointment _showItem;
        private Appointment _selectedItem;
        
        
        private RelayCommand _submitCommand;
        private RelayCommand _cancelCommand;
        private RelayCommand _saveCommand;
        

        private AppointmentController _appointmentController;
        private RoomControoler _roomController;
        private ObservableCollection<Appointment> _generatedAppointments;
        public ObservableCollection<Appointment> GeneratedAppointments
        {
            get
            {
                return _generatedAppointments;
            }
            set
            {
                _generatedAppointments = value;
                OnPropertyChanged(nameof(GeneratedAppointments));
            }
        }


        public EditAppVM(Appointment appointment, ObservableCollection<Appointment> appointmentItems)
        {
            InitializeControllers();
            _showItem = appointment;
            DoctorData = _showItem.Doctor;
            PatientData = _showItem.Patient;
        }
      
        private void InitializeControllers()
        {
            var app = System.Windows.Application.Current as App;

            _appointmentController = app.AppointmentController;
            _roomController = app.RoomController;

        }

         public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public Doctor DoctorData
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
                OnPropertyChanged(nameof(DoctorData));
            }
        }
        public Patient PatientData
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(PatientData));
            }
        }

        public Appointment SelectedItem
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

        public Appointment ShowItem
        {
            get
            {
                return _showItem;
            }
            set
            {
                _showItem = value;
                OnPropertyChanged(nameof(ShowItem));
            }
        }



        public RelayCommand SubmitCommand
        {

            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand(param => SubmitCommandExecute()));
            }

        }

        private void SubmitCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);

            GenerateAppointmentsForPriorityDoctor(startDateOnly, endDateOnly);
            
        }

        private void GenerateAppointmentsForPriorityDoctor(DateOnly startDateOnly, DateOnly endDateOnly)
        {
            GeneratedAppointments = new ObservableCollection<Appointment>(_appointmentController.GenerateAvailableAppointments
            (startDateOnly,
                endDateOnly,
                DoctorData,
                _patient,
                ExaminationType.GENERAL, _roomController.Get(3),1));
            
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => SaveCommandExecute(), param => CanSaveCommandExecute()));
            }
        }

        private bool CanSaveCommandExecute()
        {
            return SelectedItem != null;
        }

        public virtual void SaveCommandExecute()
        {
            SelectedItem.Id = _showItem.Id;
            _appointmentController.Update(SelectedItem);
            ShowItem.Date = SelectedItem.Date;
            MessageBox.Show("Appointment updated!", "note", MessageBoxButton.OK);
        }


    }
}
