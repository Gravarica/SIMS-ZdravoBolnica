using System;
using System.Collections.ObjectModel;
using System.Linq;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.WardenForms.Views;

namespace HospitalProject.View.WardenForms.ViewModels
{
    public class EquipementViewModel : BaseViewModel
    {
        private Equipement selectedItem;
        
        public ObservableCollection<Equipement> EquipementItems { get; set; }
        private EquipementController _equipementController;
        public RelayCommand EquipementRelocationCommand { get; set; }
        public RelayCommand EquipmentPdfCommand { get; set; }
        public event EventHandler OnRequestClose;

        public Equipement SelectedItem
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
        public EquipementViewModel()
        {
            InstantiateControllers();
            InstantiateData();
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _equipementController = app.EquipementController;
            EquipementRelocationCommand = new RelayCommand(param => ExecuteEquipementRelocationComand(), param => CanExecuteRelocation());
            EquipmentPdfCommand = new RelayCommand(param => ExecutePdf(), param => true);

        }

        private void ExecutePdf()
        {
            new PdfEquipmentSuplies();
            OnRequestClose?.Invoke(this, EventArgs.Empty);
        }

        private bool CanExecuteRelocation()
        {
            return SelectedItem != null;
        }
        private void ExecuteEquipementRelocationComand()
        {
            
            WardenEquipemntRelocationViewModel wardenEquipemntRelocationViewModel =
                new WardenEquipemntRelocationViewModel(selectedItem);
            MainViewModel.Instance.MomentalView = wardenEquipemntRelocationViewModel;
        }

        private void InstantiateData()
        {
            EquipementItems = new ObservableCollection<Equipement>(_equipementController.GetAll().ToList());
        }

    }
}

