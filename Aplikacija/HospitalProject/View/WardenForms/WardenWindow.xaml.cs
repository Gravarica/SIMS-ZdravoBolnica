using System.Windows;

namespace HospitalProject.View.WardenForms;

public partial class WardenWindow : Window
{
    public WardenWindow()
    {
        DataContext = new MainViewModel();
        InitializeComponent();
    }

    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {

    }
}