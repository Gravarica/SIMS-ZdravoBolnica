using HospitalProject.Controller;
using HospitalProject.Core;
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
        private string _description;
        private AnamnesisController _anamnesisController;

        private RelayCommand addNewAnamnesis;
        private RelayCommand cancelNewAnamnesis;

        public AnamnesisViewModel(Appointment appointment)
        {
            var app = System.Windows.Application.Current as App;

            ShowItem = appointment;

            _anamnesisController = app.AnamnesisController;

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

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public RelayCommand AddNewAnamnesis
        {
            get
            {
                return addNewAnamnesis ?? (addNewAnamnesis = new RelayCommand(param => AddNewAnamnesisExecute(),
                                                                               param => CanAddNewAnamnesisExecute()));
            }
        }

        private bool CanAddNewAnamnesisExecute()
        {
            return Description != null;
        }

        private void AddNewAnamnesisExecute()
        {
            _anamnesis = new Anamnesis(ShowItem, Description);
            _anamnesisController.Create(_anamnesis);
            Description = null;
        }
        
    }
}
