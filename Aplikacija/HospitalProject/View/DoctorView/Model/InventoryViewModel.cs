using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.View.DoctorView.Model
{
    public class InventoryViewModel : BaseViewModel
    {

        private EquipementController equipmentController;
        private Equipement selectedItem;

        public ObservableCollection<Equipement> Medicines { get; set; }

        public InventoryViewModel()
        {
            InstantiateControllers();
            Medicines = new ObservableCollection<Equipement>(equipmentController.GetAllMedicine());
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            equipmentController = app.EquipementController;
        }

        public Equipement SelectedMedicine
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedMedicine));
            }
        }

    }
}
