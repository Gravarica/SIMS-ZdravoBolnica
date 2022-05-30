using System.Collections.ObjectModel;
using System.Windows.Controls;
using HospitalProject.View.Secretary.SecretaryVM;
using Model;

namespace HospitalProject.View.Secretary.SecretaryV;

public partial class NewAppointmentsV : UserControl
{
    private static NewAppointmentsV _instance;
    private ObservableCollection<Appointment> _appointments;
    public NewAppointmentsV()
    {
        InitializeComponent();
        this.DataContext = new NewAppointmentsVM();
    } 

}