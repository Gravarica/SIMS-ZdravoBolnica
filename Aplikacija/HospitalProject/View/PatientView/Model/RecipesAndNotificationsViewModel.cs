using HospitalProject.Controller;
using HospitalProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.PatientView.Model
{
    public class RecipesAndNotificationsViewModel : BaseViewModel
    {

        private UserController userController;

        private Window window;

        public RelayCommand NotificationsViewCommand { get; set; }

        public RelayCommand RecipesViewCommand { get; set; }

        private RelayCommand logoutCommand;

        public RecipesViewModel RecipesVM { get; set; }

        public NotificationsViewModel NotificationsVM { get; set; }

        private object _currentView;

        

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

        public RecipesAndNotificationsViewModel(Window window)
        {
            var app = System.Windows.Application.Current as App;
            userController = app.UserController;
            
            this.window = window;

            

            NotificationsVM = new NotificationsViewModel();
            RecipesVM = new RecipesViewModel();
            CurrentView = NotificationsVM;

            NotificationsViewCommand = new RelayCommand(o =>
            {
                CurrentView = NotificationsVM;
            });

            RecipesViewCommand = new RelayCommand(o =>
            {
                CurrentView = RecipesVM;
            });
        }




    }
}
