using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
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
    public class RequestsViewModel : BaseViewModel
    {

        VacationRequestController vacationRequestController;

        private Doctor loggedDoctor;

        public ObservableCollection<VacationRequest> VacationRequests { get; set; }

        private RelayCommand newRequestCommand;

        private VacationRequest selectedRequest;

        public RequestsViewModel(Doctor loggedDoctor)
        {
            InstantiateControllers();
            InstantiateData(loggedDoctor);
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            vacationRequestController = app.VacationRequestController;
        }

        private void InstantiateData(Doctor loggedDoctor)
        {
            this.loggedDoctor = loggedDoctor;
            VacationRequests = new ObservableCollection<VacationRequest>(vacationRequestController.GetVacationRequestsForDoctor(loggedDoctor));
        }

        public VacationRequest SelectedRequest
        {
            get
            {
                return selectedRequest;
            }
            set
            {
                selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        public RelayCommand NewRequestCommand
        {
            get
            {
                return newRequestCommand ?? (newRequestCommand = new RelayCommand(param => NewRequestCommandExecute(),
                                                                                  param => NewRequestCommandCanExecute()));
            }
        }

        private void NewRequestCommandExecute()
        {
            /*NewRequestView view = new NewRequestView();
            view.DataContext = new NewRequestViewModel(loggedDoctor,vacationRequestController,view);*/
            NewRequestViewModel newRequestVM = new NewRequestViewModel(loggedDoctor, vacationRequestController);
            MainViewModel.Instance.CurrentView = newRequestVM;
            //view.ShowDialog();
        }

        private bool NewRequestCommandCanExecute()
        {
            return true;
        }

    }
}
