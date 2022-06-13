using System.Windows.Controls;
using HospitalProject.View.Secretary.SecretaryVM;

namespace HospitalProject.View.Secretary.SecretaryV;

public partial class DemoV : UserControl
{
    public DemoV()
    {
        InitializeComponent();
        this.DataContext = new DemoVM();
    }
}