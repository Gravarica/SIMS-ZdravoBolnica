using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalProject.Core;
using Model;

namespace HospitalProject.View.DoctorView.Model
{
    public class MyProfileViewModel : BaseViewModel
    {
        private Doctor logDoctor;

        public MyProfileViewModel(Doctor loggedDoctor)
        {
            LoggedDoctor = loggedDoctor;
        }

        public Doctor LoggedDoctor
        {
            get
            {
                return logDoctor;
            }
            set
            {
                logDoctor = value;
                OnPropertyChanged(nameof(LoggedDoctor));
            }
        }
    }
}
