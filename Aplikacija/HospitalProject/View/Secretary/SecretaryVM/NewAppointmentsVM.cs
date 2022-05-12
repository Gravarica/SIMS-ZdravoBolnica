using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.Secretary.SecretaryV;
using HospitalProject.View.Util;
using Model;
using Repository;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class NewAppointmentsVM : BaseViewModel
    {

        public RelayCommand Edit { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        AppointmentController _appointmentController;
        private Appointment selectedItem;
        public RelayCommand Delete { get; }
        public Appointment SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

       

        public NewAppointmentsVM()
        {

            var app = System.Windows.Application.Current as App;
            _appointmentController = app.AppointmentController;
            Appointments = new ObservableCollection<Appointment>(_appointmentController.GetAllUnfinishedAppointments());
            Edit = new RelayCommand(param => EditAppointmentCommandExecute(), param => CanExecute());
            Delete = new RelayCommand(param => DeleteCommandExecute(), param => CanExecute());

        }

        private bool CanExecute()
        {
            if (selectedItem == null)
            {
                return false;
            }

            return true;
        }

        private void DeleteCommandExecute()
        {
            if (MessageBox.Show("Are you sure you want to cancel an appointment?", "Cancel an appointment", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _appointmentController.Delete(SelectedItem.Id);
                Appointments.Remove(SelectedItem);
            }

        }
        private void EditAppointmentCommandExecute()
        {
            EditAppV view = new EditAppV();
            view.DataContext = new EditAppVM(SelectedItem, Appointments);
            view.ShowDialog();
        }

       
    }
}