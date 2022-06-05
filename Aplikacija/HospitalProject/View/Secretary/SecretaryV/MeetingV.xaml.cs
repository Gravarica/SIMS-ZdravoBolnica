using System.Windows.Controls;
using HospitalProject.View.Secretary.SecretaryVM;

namespace HospitalProject.View.Secretary.SecretaryV;

public partial class MeetingV : UserControl
{
    public MeetingV()
    {
        InitializeComponent();
        this.DataContext = new MeetingVM();
    }
}