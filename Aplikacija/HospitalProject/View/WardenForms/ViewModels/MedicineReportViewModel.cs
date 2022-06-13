using System.Collections.ObjectModel;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.WardenForms.Views;

namespace HospitalProject.View.WardenForms.ViewModels
{

    public class MedicineReportViewModel : ViewModelBase
    {
        private MedicineReport selectedItem;

        public ObservableCollection<MedicineReport> MedicineReportItems { get; set; }
        private MedicineReportController _medicineReportController;

        public RelayCommand EditMedicineCommand { get; set; }

        public MedicineReport SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public MedicineReportViewModel()
        {
            
            InstantiateControllers();
            InstantiateData();
            InitialiseCommands();
        }

        public void InitialiseCommands()
        {
            EditMedicineCommand = new RelayCommand(param => ExecuteEditMedicineCommand(), param => CanExecuteExecuteEditMedicineCommand());
        }

        private void ExecuteEditMedicineCommand()
        {
            EditMedicineView editMedicineView = new EditMedicineView(SelectedItem);
            MainViewModel.Instance.MomentalView = editMedicineView;
        }

        private bool CanExecuteExecuteEditMedicineCommand()
        {
            return SelectedItem != null;
        }
        

        private void InstantiateData()
        {
            MedicineReportItems = new ObservableCollection<MedicineReport>(_medicineReportController.GetAll());
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _medicineReportController = app.MedicineReportController;
        }
    }
}