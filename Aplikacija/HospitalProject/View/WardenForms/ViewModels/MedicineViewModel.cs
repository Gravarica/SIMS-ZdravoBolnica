using System.Collections.ObjectModel;
using System.Linq;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.WardenForms.Views;

namespace HospitalProject.View.WardenForms.ViewModels
{
    public class MedicineViewModel : BaseViewModel
    {
        public ObservableCollection<Equipement> MedicineItems { get; set; }
        private EquipementController _equipementController;
        private Equipement selectedItem;
        public RelayCommand AddMedicineCommand { get; set; }

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
        
        public MedicineViewModel()
        {
            InstantiateControllers();
            InsatiateData();
        }
        
        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _equipementController = app.EquipementController;
        }

        private void InsatiateData()
        {
            MedicineItems = new ObservableCollection<Equipement>(_equipementController.GetAllMedicine().ToList());
            AddMedicineCommand = new RelayCommand(param => ExecuteAddMedicineCommand(), param => true);

        }
        private void ExecuteAddMedicineCommand()
        {
            
            AddingMedicineView addingMedicineView = new AddingMedicineView(MedicineItems);
            MainViewModel.Instance.MomentalView = addingMedicineView;
        }
        
        
        
    }
}

