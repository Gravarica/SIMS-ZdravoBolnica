using System.Windows.Controls;
using HospitalProject.View.Secretary.SecretaryVM;

namespace HospitalProject.View.Secretary.SecretaryV;

public partial class DataBaseV : UserControl
{
    public DataBaseV()
    {
        InitializeComponent();
        this.DataContext = new DataBaseVM();
    }

  
}