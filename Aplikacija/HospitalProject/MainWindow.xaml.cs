using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalProject.Controller;
using HospitalProject.View;
using HospitalProject.View.DoctorView.Views;
using HospitalProject.View.Model;
using HospitalProject.View.PatientView;
using HospitalProject.View.PatientView.View;
using HospitalProject.View.WardenForms;

namespace HospitalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this);
        }


        private void OpenPatientView(object sender, RoutedEventArgs e)
        {
            MainPatientView mpv = new MainPatientView();
            this.Close();
            mpv.Show();
        }

        private void OpenSecretaryView(object sender, RoutedEventArgs e)
        {
            SecretaryView sv = new SecretaryView();

            sv.Show();
        }

        private void OpenWardenView(object sender, RoutedEventArgs e)
        {
            WardenWindow rv = new WardenWindow();
            this.Close();
            rv.Show();
        }   
    }
}
