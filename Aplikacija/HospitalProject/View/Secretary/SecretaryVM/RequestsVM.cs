using System.Collections.ObjectModel;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.DoctorView.Model;
using HospitalProject.View.DoctorView.Views;
using HospitalProject.View.Secretary.SecretaryV;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM;

public class RequestsVM: ViewModelBase
{
    VacationRequestController vacationRequestController;

    private RelayCommand _preview;
    private VacationRequest _selectedRequest;
  
    public ObservableCollection<VacationRequest> VacationRequests { get; set; }
  public RequestsVM()
    {
        InstantiateControllers();
        InstantiateData();
    }

    private void InstantiateControllers()
    {
        var app = System.Windows.Application.Current as App;
        vacationRequestController = app.VacationRequestController;
    }

    private void InstantiateData()
    {
       VacationRequests = new ObservableCollection<VacationRequest>(vacationRequestController.GetVacationRequests());
    }

    public VacationRequest SelectedRequest
    {
        get
        {
            return _selectedRequest;
        }
        set
        {
            _selectedRequest = value;
            OnPropertyChanged(nameof(SelectedRequest));
        }
    }
    
    public RelayCommand PreviewCommand
    {
        get
        {
            return _preview ?? (_preview = new RelayCommand(param => ExecuteShowProfileCommand(),
                param => CanExecute()));
        }
    }

    private bool CanExecute()
    {
        return SelectedRequest!= null;
    }

    private void ExecuteShowProfileCommand()
    { 
        PreviewRequestV view = new PreviewRequestV();
       view.DataContext = new PreviewRequestVM(SelectedRequest);
       view.ShowDialog();
    }
}