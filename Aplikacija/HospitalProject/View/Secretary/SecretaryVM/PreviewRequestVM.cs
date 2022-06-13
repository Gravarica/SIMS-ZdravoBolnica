using System;
using System.Windows;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM;

public class PreviewRequestVM : ViewModelBase
{
    private DateTime _startDate;
    private DateTime _endDate;
    
    private DateTime _date;
    private Doctor _doctor;
    
    private string _description;
    private string _secretaryDescription;
    private bool _urgent;
    
    private VacationRequest _vacationRequest;
    private RelayCommand _acceptCommand;
    private RelayCommand _rejectCommand;
    private VacationRequestController _vacationRequestController;

    public PreviewRequestVM(VacationRequest vacationRequest)
    {
        var app = System.Windows.Application.Current as App;

        this._vacationRequestController = app.VacationRequestController;
        ThisVacationRequest = vacationRequest;
    }
    
    
    
    public VacationRequest ThisVacationRequest
    {
        get
        {
            return _vacationRequest;
        }
        set
        {
            _vacationRequest = value;
            OnPropertyChanged(nameof(ThisVacationRequest));
        }
    }

    public RelayCommand AcceptCommand
        {
            get
            {
                return _acceptCommand ?? (_acceptCommand = new RelayCommand(param => ExecuteAcceptCommand()));
            }
        }

        public void ExecuteAcceptCommand()
         {
            _vacationRequestController.Accept(ThisVacationRequest);
            MessageBox.Show("Request accepted!", "note", MessageBoxButton.OK);
         }
        
        public RelayCommand RejectCommand
        {
            get
            {
                return _rejectCommand ?? (_rejectCommand = new RelayCommand(param => ExecuteRejectCommand(), param => CanRejectCommandExecute() ));
            }
        }
        private bool CanRejectCommandExecute()
        {
            return true;
        }

        public void ExecuteRejectCommand()
        {
            if ( String.IsNullOrEmpty(ThisVacationRequest.SecretaryDescription))
            {
                MessageBox.Show("Add description!", "warning", MessageBoxButton.OK);
            }
            else
            {
                _vacationRequestController.Reject(ThisVacationRequest);
                MessageBox.Show("Request rejected!", "note", MessageBoxButton.OK);
            }

           
        }
    }
