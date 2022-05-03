using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.Controller;
using HospitalProject.Core;
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


    }
}
