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
    /// Interaction logic for MainGradeView.xaml
    /// </summary>
    public partial class MainGradeView : Window
    {
        public MainGradeView()
        {
            InitializeComponent();
        }

        private void OpenGradingForHospital(object sender, RoutedEventArgs e)
        {
            HospitalSurveyView hsw = new HospitalSurveyView();
            hsw.DataContext = new HospitalSurveyViewModel(hsw);
            this.Close();
            hsw.Show();

        }

        private void OpenGradingForDoctor(object sender, RoutedEventArgs e)
        {
            AnamnesisViewPatient avp = new AnamnesisViewPatient();
            avp.DataContext = new AnamnesisViewPatientViewModel(avp);
            
            avp.Show();

        }

    }

    
}
