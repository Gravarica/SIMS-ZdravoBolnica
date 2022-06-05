using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;
using HospitalProject.ValidationRules.DoctorValidation;

namespace HospitalProject.View.Secretary.SecretaryVM;

public class MeetingVM : BaseViewModel
{
    private int _id;
    private Doctor _selectedItemForRemoving;
    private Doctor _selectedItemForAdding;
    private bool _isWardenAdded;
    private DateTime _date;
    private String _time;
    
    private RelayCommand _createMeeting;
    private RelayCommand _removeSelectedDoctorFromListCommand;
    private RelayCommand _addSelectedDoctorFromListCommand;
    
    private MeetingsController _meetingsController;
    private DoctorController _doctorController;
    public List<Doctor> Doctors { get; set; }
    public IList<Doctor> DoctorsForMeeting { get; set; }


    public MeetingVM(ObservableCollection<Doctor> doctorsForMeeting)
    {   
            var app = System.Windows.Application.Current as App;
            _meetingsController = app.MeetingsController;
            _doctorController = app.DoctorController;
            Doctors = new List<Doctor>(_doctorController.GetAll().ToList());
            DoctorsForMeeting = doctorsForMeeting;
    }
    
    public MeetingVM()
    {
       
        var app = System.Windows.Application.Current as App;
        _meetingsController = app.MeetingsController;
        _doctorController = app.DoctorController;
        Doctors = new List<Doctor>(_doctorController.GetAll().ToList());
        DoctorsForMeeting = new ObservableCollection<Doctor>();
    }
   
    public Doctor SelectedItemForRemoving
    {
        get{ return _selectedItemForRemoving; }
        set 
        {
            _selectedItemForRemoving = value; 
            OnPropertyChanged(nameof(SelectedItemForRemoving)); 
        }
    }
    
    public Doctor SelectedItemForAdding
    {
        get{ return _selectedItemForAdding; }
        set 
        {
            _selectedItemForAdding = value; 
            OnPropertyChanged(nameof(SelectedItemForAdding)); 
        }
    }
    public DateTime Date
    {
        get { return _date; }
        set
        { 
            _date = value;
            OnPropertyChanged(nameof(Date)); 
        }
    }
   
    public String Time
    {
        get { return _time; }
        set
        { 
            _time = value;
            OnPropertyChanged(nameof(Time)); 
        }
    }

    public DateTime CreateDateTime()
    {
        TimeOnly TimeOnly = TimeOnly.Parse(Time);
        DateOnly DateOnly = new DateOnly(Date.Year, Date.Month, Date.Day);
        return new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, TimeOnly.Hour, TimeOnly.Minute, TimeOnly.Second);
    }

    public bool IsWardenAdded
    {
        get { return _isWardenAdded; }
        set
        {
            _isWardenAdded = value;
            OnPropertyChanged(nameof(IsWardenAdded));
        }
    }


    public RelayCommand CreateMeeting
    {
        get
        {
            return _createMeeting ?? (_createMeeting = new RelayCommand(param => ExecuteCreateMeetingCommand(),
                param => CanExecuteCreateMeeting()));
        }
    }

    public RelayCommand Remove
    {
        get
        {
            return _removeSelectedDoctorFromListCommand ?? (_removeSelectedDoctorFromListCommand = new RelayCommand(
                param => ExecuteRemoveDoctorCommand(),
                param => CanExecuteRemoveDoctor()));
        }
    }

    public RelayCommand Add
    {
        get
        {
            return _addSelectedDoctorFromListCommand ?? (_addSelectedDoctorFromListCommand = new RelayCommand(
                param => ExecuteAddDoctorCommand(),
                param => CanExecuteAddDoctor()));
        }
    }
    private bool CanExecuteCreateMeeting()
    {
        //TimeFormatValidation
        return DoctorsForMeeting.Count != 0;
    }

    private bool CanExecuteAddDoctor()
    {
        return (SelectedItemForAdding!=null && DoctorAlreadyAdded());
    }
    
    private bool DoctorAlreadyAdded()
    {
        foreach (Doctor doctor in DoctorsForMeeting)
        {
            if (doctor.Id == SelectedItemForAdding.Id)
            {
                return false;
            }
        }
        return true ;
    }
    private bool CanExecuteRemoveDoctor()
    {
        return SelectedItemForRemoving!=null;
    }
    
    private void ExecuteCreateMeetingCommand()
    {
        _meetingsController.Create(new Meetings( _id, 4 , DoctorsForMeeting.ToList(), CreateDateTime(), 60, IsWardenAdded ));
        DoctorsForMeeting.Clear();
    }
    
    private void ExecuteAddDoctorCommand()
    {
        DoctorsForMeeting.Add(SelectedItemForAdding);
    }
    private void ExecuteRemoveDoctorCommand()
    {
        DoctorsForMeeting.Remove(SelectedItemForRemoving);
    }
  
    }