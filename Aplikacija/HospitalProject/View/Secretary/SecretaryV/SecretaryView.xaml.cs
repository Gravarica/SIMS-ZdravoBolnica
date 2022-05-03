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
using System.Windows.Shapes;
using HospitalProject.View.Secretary.SecretaryVM;

namespace HospitalProject.View.Secretary.SecretaryV
{
    /// <summary>
    /// Interaction logic for SecretaryView.xaml
    /// </summary>
    public partial class SecretaryView : Window
    {
        public SecretaryView()
        {
            InitializeComponent();

            this.DataContext = new SecretaryViewVM();

        }

      

        private void _AddPatient__Click(object sender, RoutedEventArgs e)
        {
            AddPatient view = new AddPatient();
            
            view.ShowDialog();
        }

        private void addGuest_Click(object sender, RoutedEventArgs e)
        {
            AddGuestPatient view = new AddGuestPatient();
          
            view.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorsList view = new DoctorsList();

            view.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Appointments view = new Appointments();

            view.ShowDialog();
        }
    }
}
