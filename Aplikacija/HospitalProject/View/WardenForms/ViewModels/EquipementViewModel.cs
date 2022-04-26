using System.Collections.ObjectModel;
using System.Linq;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;

namespace HospitalProject.View.WardenForms
{
    public class EquipementViewModel : BaseViewModel
    {
        private Equipement selectedItem;
        
        public ObservableCollection<Equipement> EquipementItems { get; set; }
        private EquipementController _equipementController;


        public EquipementViewModel()
        {
            InstantiateControllers();
            InstantiateData();
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _equipementController = app.EquipementController;
        }

        private void InstantiateData()
        {
            EquipementItems = new ObservableCollection<Equipement>(_equipementController.GetAll().ToList());
        }

    }
}

