using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HospitalProject.Repository;
using HospitalProject.View.Secretary.SecretaryVM;
using Model;

namespace HospitalProject.View.Secretary.SecretaryV
{
    /// <summary>
    /// Interaction logic for SecretaryView.xaml
    /// </summary>
    public partial class SecretaryView : Window
    {
        SecretaryViewVM secretaryVM;

        public SecretaryView()
        {
            InitializeComponent();
            secretaryVM = new SecretaryViewVM(this);
            this.DataContext = secretaryVM;

        }


        UserRepository userRepository;
        private void _AddPatient__Click(object sender, RoutedEventArgs e)
        {
            var app = System.Windows.Application.Current as App;
            AddPatient view = new AddPatient();
            AddPatientVM viewModel = new AddPatientVM(view);
            view.DataContext = viewModel ;
            view.ShowDialog();
            if(viewModel.ModalResult == true)
            {
                secretaryVM.Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            }
        }

        private void addGuest_Click(object sender, RoutedEventArgs e)
        {
            var app = System.Windows.Application.Current as App;
            AddGuestPatient view = new AddGuestPatient();
            AddGuestVM viewModel = new AddGuestVM(view);
            view.DataContext = viewModel;
            view.ShowDialog();
            if (viewModel.ModalResult == true)
            {
                secretaryVM.Patients = new ObservableCollection<Patient>(app.PatientController.GetAll());
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorsList view = new DoctorsList();

            view.ShowDialog();
        }

       
    }
}
