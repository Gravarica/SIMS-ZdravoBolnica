using System.Collections.ObjectModel;
using System.Linq;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;

namespace HospitalProject.View.WardenForms.ViewModels
{
    public class MedicineViewModel : BaseViewModel
    {
        public ObservableCollection<Equipement> MedicineItems { get; set; }
        private EquipementController _equipementController;
        private Equipement selectedItem;

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
        }
        
        
    }
}

