using System;
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
    
    public DateTime StartDate
    {
        get
        {
            return _startDate;
        }
        set
        {
            _startDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }
    public DateTime SubmissionDate
    {
        get
        {
            return _date;
        }
        set
        {
            _date = value;
            OnPropertyChanged(nameof(SubmissionDate));
        }
    }

    public DateTime EndDate
    {
        get
        {
            return _endDate;
        }
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

    public Doctor DoctorData
    {
        get
        {
            return _doctor;
        }
        set
        {
            _doctor = value;
            OnPropertyChanged(nameof(DoctorData));
        }
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

    public bool Urgent
    {
        get { return _urgent; }
        set
        {
            _urgent = value;
            OnPropertyChanged(nameof(Urgent));
        }
    }
    
    public string Description 
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    public string SecretaryDescription 
    {
        get { return _secretaryDescription; }
        set
        {
            _secretaryDescription = value;
            OnPropertyChanged(nameof(SecretaryDescription));
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
         }
        
        public RelayCommand RejectCommand
        {
            get
            {
                return _rejectCommand ?? (_rejectCommand = new RelayCommand(param => ExecuteRejectCommand()));
            }
        }
        private bool CanRejectCommandExecute()
        {
            return !string.IsNullOrEmpty(ThisVacationRequest.SecretaryDescription);
        }

        public void ExecuteRejectCommand()
        {
            _vacationRequestController.Reject(ThisVacationRequest);
        }
    }
