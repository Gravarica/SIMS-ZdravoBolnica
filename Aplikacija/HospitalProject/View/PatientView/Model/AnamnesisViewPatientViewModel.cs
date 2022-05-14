using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.PatientView.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.PatientView.Model
{
    public class AnamnesisViewPatientViewModel : BaseViewModel
    {

        public ObservableCollection<Anamnesis> Anamneses { get; set; }

        private RelayCommand editAnamnesisCommand;

        private Anamnesis selectedItem;
        private PatientController patientController;
        private UserController userController;

        private AnamnesisController anamnesisController;
        private MedicalRecord medicalRecord;
        private Window window;
        private RelayCommand doctorSurveyCommand;

        public AnamnesisViewPatientViewModel(Window window)
        {
            
            
            this.window = window;
            var app = System.Windows.Application.Current as App;
            patientController = app.PatientController;
            userController = app.UserController;
            medicalRecord = app.MedicalRecordController.GetMedicalRecordByPatient(patientController.GetLoggedPatient(userController.GetLoggedUser().Username));
            anamnesisController = app.AnamnesisController;
            Anamneses = new ObservableCollection<Anamnesis>(anamnesisController.GetAnamnesisByMedicalRecord(medicalRecord.Patient.Id));
        }

        
        public Anamnesis SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommand DoctorSurveyCommand
        {

            get
            {
                return doctorSurveyCommand ?? (doctorSurveyCommand = new RelayCommand(param => DoctorSurveyCommandExecute(), param => CanDoctorSurveyCommandExecute()));
            }
        }

        private bool CanDoctorSurveyCommandExecute()
        {
            return true;
        }

        private void DoctorSurveyCommandExecute()
        {
            DoctorSurveyView dsv = new DoctorSurveyView();
            dsv.DataContext = new DoctorSurveyViewModel(dsv,SelectedItem);
            dsv.Show();
        }

       
    }
}
