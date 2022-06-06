using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.Secretary.SecretaryV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.Secretary.SecretaryV;

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
        
        public RegisterV RegisterV { get; set; }
        public NewAppointmentsV NewAppointmentsV { get; set; }
        public DataBaseV DataBaseV { get; set; }
        public RequestsV RequestsV { get; set; }
        public EmergencyV EmergencyV { get; set; }
        public MeetingV MeetingV { get; set; }
        
        
        private Window Window;
        private object currentView;
        private static SecretaryMainViewVM _instance; 
        
        UserController _userController;
        
        public static SecretaryMainViewVM Instance => _instance;
    
        public SecretaryMainViewVM(Window window)
        {
            NewViews();
            NewCommands();

            var app = System.Windows.Application.Current as App;
            _userController = app.UserController;
             _instance = this;
             
            Window = window;
            CurrentView = DataBaseV;
        }

        private void NewViews()
        {
            //DashBoard = new DashBoardV();
            RegisterV = new RegisterV();
            DataBaseV = new DataBaseV();
            EmergencyV = new EmergencyV();
            NewAppointmentsV = new NewAppointmentsV();
            MeetingV  = new MeetingV();
            RequestsV = new RequestsV();
        }

        private void NewCommands()
        {
            RegisterCommand = new RelayCommand(o =>
                {
                    CurrentView = RegisterV;
                }
            );
            MeetingCommand = new RelayCommand(o =>
                {
                    CurrentView = MeetingV;
                }
            ); 
            DataBaseCommand = new RelayCommand(o =>
                {
                    CurrentView = DataBaseV;
                }
            );
            EmergencyCommand = new RelayCommand(o =>
                {
                    CurrentView = EmergencyV;
                }
            );
            NewAppointmentCommand = new RelayCommand(o =>
            {
                CurrentView = NewAppointmentsV;
            });
             
            RequestsCommand = new RelayCommand(o =>
            {
                CurrentView = RequestsV;
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