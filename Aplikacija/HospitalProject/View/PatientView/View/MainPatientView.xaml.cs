using HospitalProject.View.PatientView.Model;
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

namespace HospitalProject.View.PatientView.View
{
    /// <summary>
    /// Interaction logic for MainPatientView.xaml
    /// </summary>
    public partial class MainPatientView : Window
    {
        public MainPatientView()
        {
            InitializeComponent();
        }

        private void OpenAppointmentsForPatient(object sender, RoutedEventArgs e)
        {
            AppointmentsViewPatient avp = new AppointmentsViewPatient();
            this.Close();
            avp.Show();
        }

        private void OpenGrading(object sender, RoutedEventArgs e)
        {
            MainGradeView mgw = new MainGradeView();
           
            
            mgw.Show();

        }

        private void RowDefinition_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }



}
