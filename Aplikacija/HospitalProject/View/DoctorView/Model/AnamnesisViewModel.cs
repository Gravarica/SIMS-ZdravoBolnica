using HospitalProject.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class AnamnesisViewModel : ViewModelBase
    {
        private Appointment showItem;
        private Anamnesis _anamnesis;
         

        public AnamnesisViewModel(Appointment appointment)
        {
            ShowItem = appointment;
        }

        public Appointment ShowItem
        {
            get
            {
                return showItem;
            }
            set
            {
                showItem = value;
                OnPropertyChanged(nameof(ShowItem));
            }
        }

        
    }
}
