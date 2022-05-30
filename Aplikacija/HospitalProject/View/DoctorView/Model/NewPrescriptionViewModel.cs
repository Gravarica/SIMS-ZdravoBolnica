using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.ValidationRules.DoctorValidation;
using HospitalProject.View.Util;
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
        private List<ComboBoxData<int>> intervals = new List<ComboBoxData<int>>();
        private List<ComboBoxData<Equipement>> medicines = new List<ComboBoxData<Equipement>>();    
        private Appointment showItem;

        private DateTime startDate;
        private DateTime endDate;
        private int interval;
        private string description;
        private Equipement selectedMedicine;

        private PrescriptionController prescriptionController;
        private EquipementController equipementController;

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
            equipementController = app.EquipementController;
        }

        private void InstantiateData(Appointment showItem)
        {
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ShowItem = showItem;
            PatientPrescriptions = new ObservableCollection<Prescription>(prescriptionController.GetPrescriptionsForPatient(ShowItem.Patient.Id));
            FillComboData();
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

        public Equipement SelectedMedicine
        {
            get
            {
                return selectedMedicine;
            }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged(nameof(SelectedMedicine));
            }
        }

        public List<ComboBoxData<int>> IntervalComboBox
        {

            get
            {
                return intervals;
            }
            set
            {
                intervals = value;
                OnPropertyChanged(nameof(IntervalComboBox));
            }
        }

        public List<ComboBoxData<Equipement>> MedicinesComboBox
        {
            get
            {
                return medicines;
            }
            set
            {
                medicines = value;
                OnPropertyChanged(nameof(MedicinesComboBox));
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
            string checkString = prescriptionController.Create(ShowItem, StartDate, EndDate, Interval, Description, SelectedMedicine);
            if(checkString != null)
            {
                MessageBox.Show("Patient is allergic to " + checkString + " in " + SelectedMedicine.Name, "Cannot create prescription", MessageBoxButton.OK, MessageBoxImage.Warning);
            } 
            else
            {
                _window.Close();
            }
            
        }

        private void FillComboData()
        {
            FillComboDataForIntervals();
            FillComboDataForMedicine();
        }

        private void FillComboDataForIntervals()
        {
            intervals.Add(new ComboBoxData<int> { Name = "8 hours", Value = 8 });
            intervals.Add(new ComboBoxData<int> { Name = "6 hours", Value = 6 });
            intervals.Add(new ComboBoxData<int> { Name = "12 hours", Value = 12 });
        }

        private void FillComboDataForMedicine()
        {
            foreach(Equipement medicine in equipementController.GetAllMedicine())
            {
                medicines.Add(new ComboBoxData<Equipement> { Name = medicine.Name, Value = medicine });
            }
        }
    }
}
