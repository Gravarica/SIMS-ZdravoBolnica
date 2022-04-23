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
    public class EditAnamnesisViewModel : BaseViewModel
    {
        private Anamnesis showItem;
        private string _description;
        private AnamnesisController _anamnesisController;

        private RelayCommand editAnamnesisCommand;
        private RelayCommand cancelNewAnamnesis;

        public EditAnamnesisViewModel(Anamnesis anamnesis)
        {
            var app = System.Windows.Application.Current as App;

            ShowItem = anamnesis;

            _anamnesisController = app.AnamnesisController;

        }

        public Anamnesis ShowItem
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

        /*public string Description
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
        }*/

        public RelayCommand EditAnamnesisCommand
        {
            get
            {
                return editAnamnesisCommand ?? (editAnamnesisCommand = new RelayCommand(param => EditAnamnesisCommandExecute(),
                                                                               param => CanEditAnamnesisCommandExecute()));
            }
        }

        private bool CanEditAnamnesisCommandExecute()
        {
            return true;
        }

        private void EditAnamnesisCommandExecute()
        {
            _anamnesisController.Update(ShowItem);
        }


    }
}
