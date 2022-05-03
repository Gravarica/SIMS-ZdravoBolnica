using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.View.Util;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using HospitalProject.Core;
using Controller;
using HospitalProject.Controller;
using HospitalProject.View.Secretary.SecretaryV;
using HospitalProject.Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class SecretaryViewVM : BaseViewModel
    {

        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Allergies> Allergies { get; set; }

        private RelayCommand showProfileCommand;
        private RelayCommand newAppointmentCommand;
        private RelayCommand deleteCommand;
        private RelayCommand deleteAllergyCommand;
        private RelayCommand addAllergyCommand;
        private RelayCommand addPatient; 
        
        private Patient selectedItem;
        private Allergies selectedAllergy;
        private String allergyName;
        private int id;
        PatientController _patientController;
        AllergiesController _allergiesController;
       
        public SecretaryViewVM()
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;

            _allergiesController = app.AllergiesController;
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            Allergies = new ObservableCollection<Allergies>(app.AllergiesController.GetAll());
            

           // FillComboData(list);
         }

        public Patient SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(selectedItem));
            }
        }


        public Allergies SelectedAllergy
        {
            get
            {
                return selectedAllergy;
            }
            set
            {
                selectedAllergy = value;
                OnPropertyChanged(nameof(selectedAllergy));
            }
        }


        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => ExecuteDeletePatientCommand(),
                                                                                     param => CanExecute()));
            }
        }

        public RelayCommand DeleteAllergyCommand
        {
            get
            {
                return deleteAllergyCommand ?? (deleteAllergyCommand = new RelayCommand(param => ExecuteDeleteAllergyCommand(),
                                                                                     param => CanExecuteA()));
            }
        }
        public RelayCommand AddAllergyCommand
        {
            get
            {
                return addAllergyCommand ?? (addAllergyCommand = new RelayCommand(param => ExecuteaddAllergyCommand()));
            }
        }

       

        public void ExecuteaddAllergyCommand() {

            
                   Allergies aaa = _allergiesController.Create(new Allergies(id, AllergyName));
                   Allergies.Add(aaa); 
            
        }
        private void ExecuteDeletePatientCommand()
        {
            if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                
                _patientController.Delete(SelectedItem.Id);
                Patients.Remove(SelectedItem); 
                
            }

        }

        private void ExecuteDeleteAllergyCommand() {

            if (MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                _allergiesController.Delete(SelectedAllergy.Id);
                Allergies.Remove(SelectedAllergy);

            }

        }

        private bool CanExecute()
        {
            return SelectedItem != null;
        }

        private bool CanExecuteA()
        {
            return SelectedAllergy != null;
        }
        public RelayCommand ShowProfileCommand
        {
            get
            {
                return showProfileCommand ?? (showProfileCommand = new RelayCommand(param => ExecuteShowProfileCommand(),
                                                                                     param => CanExecute()));
            }
        }

        private void ExecuteShowProfileCommand()
        {

            PatientProfile view = new PatientProfile();
            view.DataContext = new PatientProfileVM(SelectedItem);
            view.ShowDialog();
        }



        /*public RelayCommand NewAppointmentCommand
        {
            get
            {
                return newAppointmentCommand ?? (newAppointmentCommand = new RelayCommand(param => ExecuteNewAppointmentCommand(),
                    param => CanExecute()));
            }
        }
        private bool CanExecuteNewAppointmentCommand()
        {
            return SelectedItem != null;
        }

        private void ExecuteNewAppointmentCommand()
        {
            return;
        } 
        */
        // Definiton of methods for Relay commands



        public string AllergyName {
            get { return allergyName; }
            set
            {
                allergyName = value;
                OnPropertyChanged(nameof(AllergyName));
            }

        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

      
    }
    }

