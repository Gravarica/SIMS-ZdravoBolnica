using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.DataTransferObjects;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.DoctorView.Model
{
    public class NewRequestViewModel : BaseViewModel
    { 

        private VacationRequestController vacationRequestController;
        private Doctor loggedDoctor;
        private Window window;

        private int numberOfFreeDays;
        private DateTime startDate;
        private DateTime endDate;
        private bool isUrgent;
        private string description;

        private RelayCommand sendCommand;
        private RelayCommand cancelCommand;

        public NewRequestViewModel(Doctor doctor, VacationRequestController vacReqContr)
        {
            loggedDoctor = doctor;
            this.vacationRequestController = vacReqContr;
        }

        public NewRequestViewModel()
        {
        }

        public RelayCommand SendCommand
        {
            get
            {
                return sendCommand ?? (sendCommand = new RelayCommand(param => SendCommandExecute(), param => CanSendCommandExecute()));
            }
        }

        private void SendCommandExecute()
        {
            if (!vacationRequestController.Create(new NewRequestDTO(DateTime.Now, LoggedDoctor, StartDate, EndDate, Description, IsUrgent)))
            {
                MessageBox.Show("There are already two doctors scheduled for vacation in that date interval. Please choose another one.", "Submission failed", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                MainViewModel.Instance.CurrentView = MainViewModel.Instance.InventoryVM;
            }
           
        }

        private bool CanSendCommandExecute()
        {
            return true;
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(param => CancelCommandExecute(), param => CanCancelCommandExecute()));
            }
        }

        private void CancelCommandExecute()
        {
            MainViewModel.Instance.CurrentView = MainViewModel.Instance.RequestsVM;
        }

        private bool CanCancelCommandExecute()
        {
            return true;
        }

        public int NumberOfFreeDays
        {
            get
            {
                return numberOfFreeDays;
            }
            set
            {
                numberOfFreeDays = value;
                OnPropertyChanged(nameof(NumberOfFreeDays));
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public bool IsUrgent
        {
            get
            {
                return isUrgent;
            }
            set
            {
                isUrgent = value;
                OnPropertyChanged(nameof(IsUrgent));
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
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
    }
}
