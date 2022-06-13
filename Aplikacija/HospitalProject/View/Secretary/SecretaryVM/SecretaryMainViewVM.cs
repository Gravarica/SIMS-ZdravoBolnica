using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.Secretary.SecretaryV;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.Secretary.SecretaryV;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    public class SecretaryMainViewVM : BaseViewModel
    {
       
        public RelayCommand RegisterCommand { get; set; }
        public RelayCommand logoutCommand { get; set; }
        public RelayCommand DataBaseCommand { get; set; }
        public RelayCommand EmergencyCommand { get; set; }
        public RelayCommand MeetingCommand { get; set; }
        public RelayCommand NewAppointmentCommand { get; set; }
        public RelayCommand RequestsCommand { get; set; }
        public RelayCommand StartDemo { get; set; }
        public RegisterVM RegisterVM { get; set; }
        public NewAppointmentsVM NewAppointmentsVM { get; set; }
        public DataBaseVM DataBaseVM { get; set; }
        public RequestsVM RequestsVM { get; set; }
        public EmergencyVM EmergencyVM { get; set; }
        public MeetingVM MeetingVM { get; set; }

        private Window Window;
        private object currentView;
        private static SecretaryMainViewVM _instance; 
        
        private UserController _userController;
        
        private EquipementController _equipmentController;
        private AllergiesController _allergiesController;
        private PatientController _patientController;
        private DoctorController _doctorController;
        private MeetingsController _meetingsController;
        

        public static SecretaryMainViewVM Instance => _instance;
    
        public SecretaryMainViewVM(Window window)
        {
            NewViews();
            NewCommands();
            
            var app = System.Windows.Application.Current as App;
           
            _userController = app.UserController;
             _instance = this;
             
            Window = window;
            CurrentView = DataBaseVM;
        }


        private void NewViews()
        {
            //DashBoard = new DashBoardV();
            RegisterVM = new RegisterVM();
            DataBaseVM = new DataBaseVM();
            EmergencyVM = new EmergencyVM();
            NewAppointmentsVM = new NewAppointmentsVM();
            MeetingVM  = new MeetingVM();
            RequestsVM = new RequestsVM();
        }

        private void NewCommands()
        {
            RegisterCommand = new RelayCommand(o =>
                {
                    CurrentView = RegisterVM;
                }
            );
            MeetingCommand = new RelayCommand(o =>
                {
                    CurrentView = MeetingVM;
                }
            ); 
            DataBaseCommand = new RelayCommand(o =>
                {
                    
                    CurrentView = DataBaseVM;
                }
            );
            EmergencyCommand = new RelayCommand(o =>
                {
                    CurrentView = EmergencyVM;
                }
            );
            NewAppointmentCommand = new RelayCommand(o =>
            {
                CurrentView = NewAppointmentsVM;
            });
             
            RequestsCommand = new RelayCommand(o =>
            {
                CurrentView = RequestsVM;
            });
           
        }
        
            
        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        
        public static SecretaryMainViewVM GetInstance()
        {
            return _instance;
        }

        public RelayCommand LogoutCommand
        {
            get
            {
                return logoutCommand ?? (logoutCommand = new RelayCommand(param => LogoutCommandExecute(), param => CanLogoutCommandExecute()));
            }
        }

        private bool CanLogoutCommandExecute()
        {
            return true;
        }

        private void LogoutCommandExecute()
        {
            _userController.Logout();
            Window.Close();
            System.Windows.Application.Current.MainWindow.Show();
        }


    }
}