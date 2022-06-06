using System.Windows.Controls;
using HospitalProject.View.Secretary.SecretaryVM;

namespace HospitalProject.View.Secretary.SecretaryV;

public partial class RequestsV : UserControl
{
    public RequestsV()
    {
        InitializeComponent();
        this.DataContext = new RequestsVM();
    }
}