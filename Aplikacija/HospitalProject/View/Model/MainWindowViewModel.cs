using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.DoctorView.Views;
using HospitalProject.View.DoctorView.Model;
using HospitalProject.View.WardenForms;

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HospitalProject.View.Secretary.SecretaryV;
using HospitalProject.View.Secretary.SecretaryVM;
using HospitalProject.View.PatientView.View;
using HospitalProject.View.PatientView.Model;

namespace HospitalProject.View.Model
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string username;
        private string password;
        private Window window;

        private RelayCommand loginCommand;
        private RelayCommand exitCommand;

        private UserController userController;

        public string Username 
        { 
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public MainWindowViewModel(Window window){
            var app = System.Windows.Application.Current as App;
            userController = app.UserController;
            this.window = window;
        }

        public RelayCommand LoginCommand
        { 
            get
            {
               return loginCommand ?? (loginCommand = new RelayCommand(param => LoginCommandExecute(),
                                                                   param => CanLoginCommandExecute()));
            }
        }

        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new RelayCommand(param => ExitCommandExecute(), param => CanExitCommandExecute()));
            }
        }

        private void ExitCommandExecute()
        {
            window.Close();
        }

        private bool CanExitCommandExecute()
        {
            return true;
        }

        private void LoginCommandExecute()
        {
            User user = userController.Login(Username, Password);
            
             
            if(user != null)
            {
                if (user.IsBlocked)
                {
                    MessageBox.Show("Login failed, user is blocked.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                if (user.UserType == UserType.DOCTOR) {
                    OpenDoctorView();
                }
                else if(user.UserType == UserType.PATIENT)
                {
                    OpenPatientView();
                }
                else if(user.UserType == UserType.SECRETARY)
                {
                    OpenSecretaryView();
                }
                else
                {
                    OpenWardenView();
                }
            } 
            else
            {
                MessageBox.Show("Wrong credentials. Please try again.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanLoginCommandExecute()
        {
            return Username != null && Password != null;
        }

        private void OpenDoctorView()
        {
            MainView dv = new MainView();
            dv.DataContext = new DoctorView.Model.MainViewModel(dv);
            HideWindow();
            dv.Show();
        }

        private void OpenPatientView()
        {
            MainPatientView pv = new MainPatientView();
            HideWindow();
            pv.Show();
        }

        private void OpenSecretaryView()
        {
            SecretaryMainV sv = new SecretaryMainV();
            sv.DataContext = new SecretaryMainViewVM(sv);
            HideWindow();
            sv.Show();
        }

        private void OpenWardenView()
        {
            WardenWindow rv = new WardenWindow();
            window.Close();
            rv.Show();
        }

        private void HideWindow()
        {
            Username = null;
            Password = null;
            window.Hide();
        }
    }
}
