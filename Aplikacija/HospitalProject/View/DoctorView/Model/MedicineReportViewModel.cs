using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.DoctorView.Model
{
    public class MedicineReportViewModel : BaseViewModel
    {
        private DateTime submissionDate;
        private Doctor loggedDoctor;
        private Equipement showItem;
        private string description;

        private MedicineReportController medicineReportController;

        private RelayCommand submitCommand;
        private RelayCommand cancelCommand;

        private Window window;

        public MedicineReportViewModel(Doctor loggedDoctor, Equipement showItem, Window window)
        {
            this.window = window;
            InstantiateData(loggedDoctor,showItem);
            InstantiateControllers();
        }

        private void InstantiateData(Doctor loggedDoctor, Equipement showItem)
        {
            this.loggedDoctor=loggedDoctor;
            this.showItem=showItem;
            this.submissionDate = DateTime.Now;
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            medicineReportController = app.MedicineReportController;

        }

        public Doctor LoggedDoctor
        {
            get
            {
                return loggedDoctor;
            }
            set
            {
                loggedDoctor=value;
                OnPropertyChanged(nameof(LoggedDoctor));
            }
        }

        public DateTime SubmissionDate
        {
            get
            {
                return submissionDate;
            }
            set { submissionDate = value; OnPropertyChanged(nameof(SubmissionDate)); }
        }

        public Equipement ShowItem
        {
            get
            {
                return showItem;
            }
            set
            {
                showItem=value; OnPropertyChanged(nameof(ShowItem));
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
                description=value; OnPropertyChanged(nameof(Description));
            }
        }

        public RelayCommand SubmitCommand
        {
            get
            {
                return submitCommand ?? (submitCommand = new RelayCommand(param => SubmitCommandExecute(), param => CanSubmitCommandExecute()));
            }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(param => CancelCommandExecute(), param => CanCancelCommandExecute()));
            }
        }

        private bool CanSubmitCommandExecute()
        {
            return !string.IsNullOrEmpty(Description);
        }

        private void SubmitCommandExecute()
        {
            if(!medicineReportController.Create(ShowItem, Description, LoggedDoctor))
            {
                MessageBox.Show("There is already pending report for given medicine", "Report creation failed", MessageBoxButton.OK, MessageBoxImage.Information);
            } 
            else
            {
                window.Close();
            }
            
        }

        private bool CanCancelCommandExecute()
        {
            return true;
        }

        private void CancelCommandExecute()
        {
            window.Close();
        }


    }
}
