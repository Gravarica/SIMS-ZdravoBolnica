using System.Windows.Controls;

namespace HospitalProject.View.WardenForms;

public partial class WardenEquipementView : UserControl
{
    public WardenEquipementView()
    {
        
        InitializeComponent();
        DataContext = new EquipementViewModel();
    }
}