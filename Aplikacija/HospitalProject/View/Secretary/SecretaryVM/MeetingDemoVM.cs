using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.Secretary.SecretaryV;
using Model;
using Syncfusion.ProjIO;

using Syncfusion.ProjIO;
using Syncfusion.UI.Xaml.Scheduler;
using Task = System.Threading.Tasks.Task;
namespace HospitalProject.View.Secretary.SecretaryVM;

public class MeetingDemoVM: BaseViewModel
{
    private int _id;
    private Doctor _selectedItemForRemoving;
    private Doctor _selectedItemForAdding;
    private bool _isWardenAdded;
    private DateTime _date;
    private TimeOnly _time;
    private RelayCommand _createMeeting;
    private RelayCommand _removeSelectedDoctorFromListCommand;
    private RelayCommand _addSelectedDoctorFromListCommand;

    private RelayCommand _startDemo;
    private MeetingsController _meetingsController;
    private DoctorController _doctorController;
    public List<Doctor> Doctors { get; set; }
    public IList<Doctor> DoctorsForMeeting { get; set; }
    private List<TimeOnly> _timeList = new List<TimeOnly>();

    private bool _isChecked;
    
    public System.Windows.Visibility _showLabel1;
    public System.Windows.Visibility _showLabel2;
    
    public System.Windows.Visibility _showLabel3;
    private RelayCommand _stopCommand;
    public MeetingDemoVM()
    {

        _showLabel1 = System.Windows.Visibility.Hidden;
            
        _showLabel2 = System.Windows.Visibility.Hidden;
        
        _showLabel3 = System.Windows.Visibility.Hidden;
        IsChecked = true;
        var app = System.Windows.Application.Current as App;
        _meetingsController = app.MeetingsController;
        _doctorController = app.DoctorController;
        Doctors = new List<Doctor>(_doctorController.GetAll().ToList());
        DoctorsForMeeting = new ObservableCollection<Doctor>();
        FillTimeCB();
        Task.Delay(3000);
        Demo();
    }

    public System.Windows.Visibility  ShowLabel1
    {
        get
        {
            return _showLabel1;
        }
        set
        {
            _showLabel1 = value;
            OnPropertyChanged(nameof(ShowLabel1));
        }
    }

    public System.Windows.Visibility  ShowLabel2
    {
        get
        {
            return _showLabel2;
        }
        set
        {
            _showLabel2 = value;
            OnPropertyChanged(nameof(ShowLabel2));
        }
    }
    public System.Windows.Visibility  ShowLabel3
    {
        get
        {
            return _showLabel3;
        }
        set
        {
            _showLabel3 = value;
            OnPropertyChanged(nameof(ShowLabel3));
        }
    }
    public bool IsChecked
    {
        get
        {
            return _isChecked;
        }
        set
        {
            _isChecked = value;
            OnPropertyChanged(nameof(IsChecked));
        }
    }
    public Doctor SelectedItemForRemoving
    {
        get { return _selectedItemForRemoving; }
        set
        {
            _selectedItemForRemoving = value;
            OnPropertyChanged(nameof(SelectedItemForRemoving));
        }
    }

    public Doctor SelectedItemForAdding
    {
        get { return _selectedItemForAdding; }
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

    public TimeOnly Time
    {
        get { return _time; }
        set
        {
            _time = value;
            OnPropertyChanged(nameof(Time));
        }
    }

    public List<TimeOnly> TimeCB
    {
        get { return _timeList; }

    }

    public void FillTimeCB()
    {
        TimeOnly time = new TimeOnly(8,0);
        for (int i = 0; i < 12; i++)
        {
            
            _timeList.Add(time.AddHours(i));
            
        }

    }

    public DateTime CreateDateTime()
    {
        DateOnly DateOnly = new DateOnly(Date.Year, Date.Month, Date.Day);
        
        return new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day,Time.Hour, Time.Minute, Time.Second);
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
        
        return DoctorsForMeeting.Count != 0;
    }

    private bool CanExecuteAddDoctor()
    {
        return (SelectedItemForAdding!=null);
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
    public async  void Demo()
    {
            
        await Task.Delay(2000);
        ShowLabel1 = System.Windows.Visibility.Visible;
        
        await Task.Delay(2000);
        SelectedItemForAdding = Doctors[1];
        ExecuteAddDoctorCommand();
        
        await Task.Delay(2000);
        
        SelectedItemForAdding = Doctors[3];
        await Task.Delay(2000);
        ExecuteAddDoctorCommand();
        
        await Task.Delay(2000);
        SelectedItemForAdding = Doctors[2];
        await Task.Delay(2000);
        ExecuteAddDoctorCommand();
        
            
        await Task.Delay(2000);
            
        ShowLabel1 = System.Windows.Visibility.Hidden;
            
        await Task.Delay(1000);
        ShowLabel2 = System.Windows.Visibility.Visible;
        
        SelectedItemForRemoving = DoctorsForMeeting[1];
        await Task.Delay(2000);
        ExecuteRemoveDoctorCommand();
        
        await Task.Delay(2000);

        IsWardenAdded = true;
        
        await Task.Delay(2000);
        
        Time = TimeCB[1];

        ShowLabel2 = System.Windows.Visibility.Hidden;
        
        await Task.Delay(2000);
        
        ShowLabel3 = System.Windows.Visibility.Visible;
        Date =  new DateTime(2022, 10, 7, 10, 0, 0);
        
        await Task.Delay(2000);
        MessageBox.Show("Meeting created!", "note", MessageBoxButton.OK);
        DoctorsForMeeting.Clear();
        IsWardenAdded = false;
    }
    public RelayCommand StopDemo
    {
        get
        {
            return _stopCommand ?? (_stopCommand = new RelayCommand(param => StopDemoExecute()));
        }
    }


    private void StopDemoExecute()
    {
        
        MeetingV view = new MeetingV();
        view.DataContext = new MeetingVM();
        SecretaryMainViewVM.Instance.CurrentView = view;
    }
    }