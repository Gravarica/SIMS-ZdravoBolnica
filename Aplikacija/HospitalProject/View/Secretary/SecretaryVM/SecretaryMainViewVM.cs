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
    internal class SecretaryMainViewVM : BaseViewModel
    {
        private Window window;
        private object currentView;
        public RelayCommand RegisterCommand { get; set; }
        public RelayCommand logoutCommand { get; set; }
        public RelayCommand DataBaseCommand { get; set; }
        public RelayCommand EmergencyCommand { get; set; }
        public RelayCommand PublicNotifficationsCommand { get; set; }
        public RegisterVM RegisterVM { get; set; }
        public RegisterV RegisterV { get; set; }
        public NewAppointmentsVM NewAppointmentsVM { get; set; }

        public NewAppointmentsV NewAppointmentsV { get; set; }
        public RelayCommand NewAppointmentCommand { get; set; }
        public DataBaseVM DataBaseVM { get; set; }
        public DataBaseV DataBaseV { get; set; }
        public EmergencyVM EmergencyVM { get; set; }

        public EmergencyV EmergencyV { get; set; }

        public RelayCommand RequestsCommand { get; set; }
        public DashBoardVM DashBoardVM { get; set; }


        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

      
        private static SecretaryMainViewVM _instance;

        public static SecretaryMainViewVM GetFirstInstance()
        {

            if (_instance == null)
            {
                _instance = new SecretaryMainViewVM();
            }
            return _instance;
        }

        public static SecretaryMainViewVM GetInstance()
        {
            return _instance;
        }
        private SecretaryMainViewVM()
        {
            //DashBoard = new DashBoardV();
            RegisterV = new RegisterV();
            DataBaseV = new DataBaseV();
            EmergencyV = new EmergencyV();
            NewAppointmentsV = new NewAppointmentsV();
            //PublicNotifficationsVM = new PublicNotifficationsVM();
            //RequestsVM = new RequestsVM();
           

            CurrentView = DataBaseV;
            RegisterCommand = new RelayCommand(o =>
            {
                CurrentView = RegisterV;
            }
            );
            /*PublicNotifficationsCommand = new RelayCommand(o =>
            {
                CurrentView = PublicNotifficationsVM;

            }
            ); */
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
            /*  RequestsCommand = new RelayCommand(o =>
              {
                  CurrentView = RequestsVM;
              }
              );*/

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


        UserController _userController;
        private void LogoutCommandExecute()
        {
            _userController.Logout();
            //window.Close();
            System.Windows.Application.Current.MainWindow.Show();
        }

    }
}
