using System.Windows.Controls;
using HospitalProject.View.Secretary.SecretaryVM;

namespace HospitalProject.View.Secretary.SecretaryV;

public partial class EmergencyV : UserControl
{
    public EmergencyV()
    {
        InitializeComponent(); 
        this.DataContext = new EmergencyVM();
    }
}