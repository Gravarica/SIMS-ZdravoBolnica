using System.Windows;
using HospitalProject.View.WardenForms.ViewModels;
using System.Windows.Controls.Primitives;

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

    private void RadioButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.PlacementTarget = Rooms;
        popup_warden.Placement = PlacementMode.Relative;
        popup_warden.HorizontalOffset = 220;
        popup_warden.VerticalOffset = -25;
        popup_warden.IsOpen = true;
        Helper.PopupText.Text = "View Rooms";

    }

    private void Rooms_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.Visibility = Visibility.Collapsed;
        popup_warden.IsOpen = false;
    }

    private void Equipment_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.PlacementTarget = Equipment;
        popup_warden.Placement = PlacementMode.Relative;
        popup_warden.HorizontalOffset = 220;
        popup_warden.VerticalOffset = -22;
        popup_warden.IsOpen = true;
        Helper.PopupText.Text = "View Equipment";
    }

    private void Equipment_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.Visibility = Visibility.Collapsed;
        popup_warden.IsOpen = false;
    }

    private void Medicine_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.PlacementTarget = Medicine;
        popup_warden.Placement = PlacementMode.Relative;
        popup_warden.HorizontalOffset = 220;
        popup_warden.VerticalOffset = -22;
        popup_warden.IsOpen = true;
        Helper.PopupText.Text = "View Medicine";
    }

    private void Medicine_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.Visibility = Visibility.Collapsed;
        popup_warden.IsOpen = false;
    }

    private void MedicineReports_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.PlacementTarget = MedicineReports;
        popup_warden.Placement = PlacementMode.Relative;
        popup_warden.HorizontalOffset = 220;
        popup_warden.VerticalOffset = -22;
        popup_warden.IsOpen = true;
        Helper.PopupText.Text = "View Medicine Reports";
    }

    private void MedicineReports_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.Visibility = Visibility.Collapsed;
        popup_warden.IsOpen = false;
    }

    private void Grades_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.PlacementTarget = Grades;
        popup_warden.Placement = PlacementMode.Relative;
        popup_warden.HorizontalOffset = 220;
        popup_warden.VerticalOffset = -22;
        popup_warden.IsOpen = true;
        Helper.PopupText.Text = "View Grades";
    }

    private void Grades_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.Visibility = Visibility.Collapsed;
        popup_warden.IsOpen = false;
    }

    private void Grades_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Grades.BorderThickness = new Thickness(1, 1, 0, 0);
    }

    private void Grades_Click(object sender, RoutedEventArgs e)
    {
        Grades.BorderThickness = new Thickness(0, 1, 0, 1);
        MedicineReports.BorderThickness = new Thickness(0, 0, 0, 0);
        Medicine.BorderThickness = new Thickness(0, 0, 0, 0);
        Equipment.BorderThickness = new Thickness(0, 0, 0, 0);
        Rooms.BorderThickness = new Thickness(0, 1, 0, 0);
        Help.BorderThickness = new Thickness(0, 0, 0, 0);
    }

    private void MedicineReports_Click(object sender, RoutedEventArgs e)
    {
        MedicineReports.BorderThickness = new Thickness(0, 1, 0, 1);
        Grades.BorderThickness = new Thickness(0, 0, 0, 0);
        Medicine.BorderThickness = new Thickness(0, 0, 0, 0);
        Equipment.BorderThickness = new Thickness(0, 0, 0, 0);
        Rooms.BorderThickness = new Thickness(0, 1, 0, 0);
        Help.BorderThickness = new Thickness(0, 0, 0, 0);
    }

    private void Medicine_Click(object sender, RoutedEventArgs e)
    {
        Medicine.BorderThickness = new Thickness(0, 1, 0, 1);
        MedicineReports.BorderThickness = new Thickness(0, 0, 0, 0);
        Grades.BorderThickness = new Thickness(0, 0, 0, 0);
        Equipment.BorderThickness = new Thickness(0, 0, 0, 0);
        Rooms.BorderThickness = new Thickness(0, 1, 0, 0);
        Help.BorderThickness = new Thickness(0, 0, 0, 0);
    }

    private void Equipment_Click(object sender, RoutedEventArgs e)
    {
        Equipment.BorderThickness = new Thickness(0, 1, 0, 1);
        Medicine.BorderThickness = new Thickness(0, 0, 0, 0);
        MedicineReports.BorderThickness = new Thickness(0, 0, 0, 0);
        Grades.BorderThickness = new Thickness(0, 0, 0, 0);
        Rooms.BorderThickness = new Thickness(0, 1, 0, 0);
        Help.BorderThickness = new Thickness(0, 0, 0, 0);
    }

    private void Rooms_Click(object sender, RoutedEventArgs e)
    {
        Rooms.BorderThickness = new Thickness(0, 1, 0, 1);
        Medicine.BorderThickness = new Thickness(0, 0, 0, 0);
        MedicineReports.BorderThickness = new Thickness(0, 0, 0, 0);
        Grades.BorderThickness = new Thickness(0, 0, 0, 0);
        Equipment.BorderThickness = new Thickness(0, 0, 0, 0);
        Help.BorderThickness = new Thickness(0, 0, 0, 0);
    }

    private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.PlacementTarget = Help;
        popup_warden.Placement = PlacementMode.Relative;
        popup_warden.HorizontalOffset = 220;
        popup_warden.VerticalOffset = -25;
        popup_warden.IsOpen = true;
        Helper.PopupText.Text = "View Commands";

      
    }

    private void Help_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        popup_warden.Visibility = Visibility.Collapsed;
        popup_warden.IsOpen = false;
    }

    

    private void Help_Click(object sender, RoutedEventArgs e)
    {
        Help.BorderThickness = new Thickness(0, 1, 0, 1);
        Medicine.BorderThickness = new Thickness(0, 0, 0, 0);
        MedicineReports.BorderThickness = new Thickness(0, 0, 0, 0);
        Grades.BorderThickness = new Thickness(0, 0, 0, 0);
        Equipment.BorderThickness = new Thickness(0, 0, 0, 0);
        Rooms.BorderThickness = new Thickness(0, 1, 0, 0);
    }
}