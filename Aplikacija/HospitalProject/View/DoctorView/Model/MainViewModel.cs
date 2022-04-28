﻿using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.DoctorView.Model;
using HospitalProject.View.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.DoctorView.Model
{
    public class MainViewModel : BaseViewModel
    {

        private UserController userController;

        private Window window;

        public RelayCommand AppointmentsViewCommand { get; set; }
        
        public RelayCommand PatientsViewCommand { get; set; }

        private RelayCommand logoutCommand;

        public MainDoctorViewModel AppVM { get; set; }

        public PatientsViewModel PatientsVM { get; set; }

        private object _currentView;

        private Doctor loggedDoctor;

        public object CurrentView
        {
            get 
            { 
                return _currentView; 
            }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }

        public Doctor LoggedDoctor
        { 
            get
            {
                return loggedDoctor;
            }
            set
            {
                loggedDoctor = value;
                OnPropertyChanged(nameof(LoggedDoctor));
            }
        }

        public RelayCommand LogoutCommand
        {
            get
            {
                return logoutCommand ?? (logoutCommand = new RelayCommand(param => LogoutCommandExecute(), param => CanLogoutCommandExecute()));
            }
        }


        public MainViewModel(Window window)
        {
            var app = System.Windows.Application.Current as App;
            userController = app.UserController;
            var _doctorController = app.DoctorController;
            this.window = window;

            loggedDoctor = _doctorController.GetLoggedDoctor(userController.GetLoggedUser().Username);

            AppVM = new MainDoctorViewModel();
            PatientsVM = new PatientsViewModel();
            CurrentView = AppVM;

            AppointmentsViewCommand = new RelayCommand(o =>
            {
                CurrentView = AppVM;
            });

            PatientsViewCommand = new RelayCommand(o =>
            {
                CurrentView = PatientsVM;
            });
        }


        private bool CanLogoutCommandExecute()
        {
            return true;
        }

        private void LogoutCommandExecute()
        {
            userController.Logout();
            window.Close();
            Application.Current.MainWindow.Show();
        }

    }
}
