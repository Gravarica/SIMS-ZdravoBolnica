using HospitalProject.Core;
using HospitalProject.View.DoctorView.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class PatientsViewModel : BaseViewModel
    {
        public ObservableCollection<Patient> Patients { get; set; }

        private RelayCommand showMedicalCardCommand;
        private RelayCommand newAppointmentCommand;

        private Patient selectedItem;

        public PatientsViewModel(Doctor loggedDoctor)
        {
            var app = System.Windows.Application.Current as App;

            Patients = new ObservableCollection<Patient>(app.PatientController.GetPatientsThatHadAppointmentWithDoctor(loggedDoctor));
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


        // Setting properties for relay commands

        public RelayCommand ShowMedicalCardCommand
        {
            get
            {
                return showMedicalCardCommand ?? (showMedicalCardCommand = new RelayCommand(param => ExecuteShowMedicalCardCommand(),
                                                                                     param => CanExecuteShowMedicalCardCommand()));
            }
        }

        public RelayCommand NewAppointmentCommand
        {
            get
            {
                return newAppointmentCommand ?? (newAppointmentCommand = new RelayCommand(param => ExecuteNewAppointmentCommand(),
                    param => CanExecuteNewAppointmentCommand()));
            }
        }

        // Definiton of methods for Relay commands
        
        private bool CanExecuteShowMedicalCardCommand()
        {
            return true;
        }

        private void ExecuteShowMedicalCardCommand()
        {
            //MedicalCardView view = new MedicalCardView();
            MedicalCardViewModel mv = new MedicalCardViewModel(SelectedItem, ReturnFlag.PATIENT_VIEW);
            MainViewModel.Instance.CurrentView = mv;
        }

        private bool CanExecuteNewAppointmentCommand()
        {
            return SelectedItem != null;
        }

        private void ExecuteNewAppointmentCommand()
        {
            return;
        }
    }
}
