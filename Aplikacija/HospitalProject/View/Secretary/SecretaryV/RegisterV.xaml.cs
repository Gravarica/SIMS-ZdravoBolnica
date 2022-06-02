using System.Windows.Controls;
using HospitalProject.View.Secretary.SecretaryVM;

namespace HospitalProject.View.Secretary.SecretaryV;

public partial class RegisterV : UserControl
{
    public RegisterV()
    {
        InitializeComponent();
        this.DataContext = new RegisterVM();
    }
}