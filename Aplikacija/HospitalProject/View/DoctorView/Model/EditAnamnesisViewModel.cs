using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.DoctorView.Model
{
    public class EditAnamnesisViewModel : BaseViewModel
    {
        private Anamnesis showItem;
        private AnamnesisController _anamnesisController;
        private Window _window;
        private bool modalResult;

        private RelayCommand editAnamnesisCommand;
        private RelayCommand exitCommand;

        public EditAnamnesisViewModel(Anamnesis anamnesis, Window window)
        {
            InstantiateControllers();
            InstantiateData(anamnesis,window);
        }

        private void InstantiateData(Anamnesis anamnesis, Window window)
        {
            ShowItem = anamnesis;
            _window = window;
            modalResult = false;
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
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

        public bool ModalResult
        {
            get
            {
                return modalResult;
            }
            set
            {
                modalResult = value;
                OnPropertyChanged(nameof(ModalResult));
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

        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new RelayCommand(param => ExitCommandExecute(),
                                                                               param => CanExitCommandExecute()));
            }
        }

        private bool CanEditAnamnesisCommandExecute()
        {
            return !string.IsNullOrEmpty(ShowItem.Description);
        }

        private void EditAnamnesisCommandExecute()
        {
            _anamnesisController.Update(ShowItem);
            modalResult = true;
            _window.Close();
        }

        private bool CanExitCommandExecute()
        {
            return true;
        }

        private void ExitCommandExecute()
        {
            _window.Close();
        }


    }
}
