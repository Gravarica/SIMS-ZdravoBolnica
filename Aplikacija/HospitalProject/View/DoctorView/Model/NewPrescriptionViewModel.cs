using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.ValidationRules.DoctorValidation;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.DoctorView.Model
{
    public class NewPrescriptionViewModel : BaseViewModel
    {
        private Window _window;

        private RelayCommand returnCommand;
        private RelayCommand saveCommand;

        public ObservableCollection<Prescription> PatientPrescriptions { get; set; }
        private Appointment showItem;

        private DateTime startDate;
        private DateTime endDate;
        private int interval;
        private string description;

        private PrescriptionController prescriptionController;

        public NewPrescriptionViewModel(Window window, Appointment showItem)
        {
            _window = window;
            InstantiateControllers();
            InstantiateData(showItem);
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            prescriptionController = app.PrescriptionController;
        }

        private void InstantiateData(Appointment showItem)
        {
            ShowItem = showItem;
            PatientPrescriptions = new ObservableCollection<Prescription>(prescriptionController.GetPrescriptionsForPatient(ShowItem.Patient.Id));
        }

        public Appointment ShowItem
        {
            get
            {
                return showItem;
            }
            set
            {
                showItem = value;
                OnPropertyChanged(nameof(ShowItem)); 
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;    
            }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(endDate));
            }
        }

        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
                OnPropertyChanged(nameof(Interval));
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public RelayCommand ReturnCommand
        {
            get
            {
                return returnCommand ?? (returnCommand = new RelayCommand(param => ReturnCommandExecute(), param => CanReturnCommandExecute()));
            }
        }

        private bool CanReturnCommandExecute()
        {
            return true;
        }

        private void ReturnCommandExecute()
        {
            _window.Close();
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => SaveCommandExecute(), param => CanSaveCommandExecute()));
            }
        }

        private bool CanSaveCommandExecute()
        {
            return NewAppointmentValidation.IsStartBeforeEnd(StartDate, EndDate) && !string.IsNullOrEmpty(Description);
        }

        private void SaveCommandExecute()
        {
            DateOnly startDateOnly = new DateOnly(StartDate.Year, StartDate.Month, StartDate.Day);
            DateOnly endDateOnly = new DateOnly(EndDate.Year, EndDate.Month, EndDate.Day);
            prescriptionController.Create(ShowItem, startDateOnly, endDateOnly, Interval, Description);
        }
    }
}
