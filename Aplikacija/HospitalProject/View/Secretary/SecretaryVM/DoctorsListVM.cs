using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.View.Secretary.SecretaryV;
using Model;

namespace HospitalProject.View.Secretary.SecretaryVM
{
    internal class DoctorsListVM : BaseViewModel
    {
        public ObservableCollection<Doctor> Doctors { get; set; }
        private RelayCommand showProfileCommand;
        private Doctor selectedItem;
        //AppointmentController _appointmentController;
        DoctorController _doctorController;
        public DoctorsListVM()
        {
            var app = System.Windows.Application.Current as App;
            _doctorController = app.DoctorController;
            Doctors = new ObservableCollection<Doctor>(app.DoctorController.GetAll());
        }
        public Doctor SelectedItem
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
        private bool CanExecute()
        {
            return SelectedItem != null;
        }

       
        public RelayCommand ShowProfileCommand
        {
            get
            {
                return showProfileCommand ?? (showProfileCommand = new RelayCommand(param => ExecuteShowProfileCommand(),
                                                                                     param => CanExecute()));
            }
        }

        private void ExecuteShowProfileCommand()
        {

            DoctorProfile view = new DoctorProfile();
            view.DataContext = new DoctorProfileVM(SelectedItem);
            view.ShowDialog();
        }


    }
}
