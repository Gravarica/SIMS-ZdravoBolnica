using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.DoctorView.Model;
using HospitalProject.View.PatientView.View;
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

        public RelayCommand NotesViewCommand { get; set; }

        public RelayCommand RecipesViewCommand { get; set; }

        public RelayCommand PrescriptionHistoryViewCommand { get; set; }

        private RelayCommand logoutCommand;

        private RelayCommand makeCustomNotificationCommand;

        public RecipesViewModel RecipesVM { get; set; }

        public NotesViewModel NotesVM { get; set; }

        public MedicalCardViewModel MedicalCardVM { get; set; }

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

            

            NotesVM = new NotesViewModel();
            RecipesVM = new RecipesViewModel();
            MedicalCardVM = new MedicalCardViewModel();
            CurrentView = NotesVM;

            NotesViewCommand = new RelayCommand(o =>
            {
                CurrentView = NotesVM;
            });

            RecipesViewCommand = new RelayCommand(o =>
            {
                CurrentView = RecipesVM;
            });

            PrescriptionHistoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = MedicalCardVM;
            });
        }

        public RelayCommand MakeCustomNotificationCommand
        {

            get
            {
                return makeCustomNotificationCommand ?? (makeCustomNotificationCommand = new RelayCommand(param => MakeCustomNotificationCommandExecute(), param => CanMakeCustomNotificationCommandExecute()));
            }

        }

        private void MakeCustomNotificationCommandExecute()
        {
            CustomNotificationView cnvw = new CustomNotificationView();
            cnvw.DataContext = new CustomNotificationViewModel(cnvw);
            cnvw.Show(); 
        }

        private bool CanMakeCustomNotificationCommandExecute()
        {
            return true;
        }
    }
}
