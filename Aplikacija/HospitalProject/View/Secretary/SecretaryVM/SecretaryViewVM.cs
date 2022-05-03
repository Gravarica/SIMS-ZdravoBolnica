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
        public ObservableCollection<Allergies> al { get; set; }

        private RelayCommand showProfileCommand;
        private RelayCommand newAppointmentCommand;
        private RelayCommand deleteCommand;
        private RelayCommand addCommand;
        private RelayCommand addAllergyCommand;
        private RelayCommand addGuestCommand;
        private Patient selectedItem;
        private String allergyName;
        private int id;
        private List<ComboBoxData<Allergies>> allergije = new List<ComboBoxData<Allergies>>();
        PatientController _patientController;
        AllergiesController allergiesController;
        MedicalRecordController _medicalRecordcontroller;
        public SecretaryViewVM()
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;

            allergiesController = app.AllergiesController;
            Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            al = new ObservableCollection<Allergies>(app.AllergiesController.GetAll());
            
            List<Allergies> list = (List<Allergies>)allergiesController.GetAll().ToList();

            FillComboData(list);
            //  deleteCommand = new RelayCommand(param => ExecuteDeleteCommand(), param => CanExecute());  
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


        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => ExecuteDeleteCommand(),
                                                                                     param => CanExecute()));
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

            
                   Allergies aaa = allergiesController.Create(new Allergies(id, AllergyName));
                   al.Add(aaa); 
            
        }
        public void ExecuteDeleteCommand()
        {


            if (MessageBox.Show("Are you sure you want to delete a patient?", " ", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {


                Patients.Remove(SelectedItem);
                _patientController.Delete(SelectedItem.Id);
            }


        }


        private bool CanExecute()
        {
            return SelectedItem != null;
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

            MedicalRecord m = _medicalRecordcontroller.GetMedicalRecordByPatient(SelectedItem);
            PatientProfile view = new PatientProfile(m);
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

        private void FillComboData(List<Allergies>list)
        {
            foreach (Allergies al in list)
            {
                allergije.Add(new ComboBoxData<Allergies> { Name = al.Name, Value = al });
            }

        }

        public List<ComboBoxData<Allergies>> AllergiesCB
        {

            get
            {
                return allergije;
            }
            set
            {
                allergije = value;
                OnPropertyChanged(nameof(AllergiesCB));
            }
        }

    }
    }

