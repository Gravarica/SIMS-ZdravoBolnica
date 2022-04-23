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
using HospitalProject.View;
using HospitalProject.View.DoctorView.Views;
using HospitalProject.View.Model;
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
        }

        private void OpenDoctorView(object sender, RoutedEventArgs e)
        {
            MainView dv = new MainView();
            this.Close();
            dv.Show();
        }

        private void OpenPatientView(object sender, RoutedEventArgs e)
        {
            PatientView pv = new PatientView();
            this.Close();
            pv.Show();
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
