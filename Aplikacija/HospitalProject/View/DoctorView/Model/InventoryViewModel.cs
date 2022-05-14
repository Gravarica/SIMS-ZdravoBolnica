using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.DoctorView.Views;
using Model;
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
        private Doctor loggedDoctor;

        public ObservableCollection<Equipement> Medicines { get; set; }

        private RelayCommand reportCommand;

        public InventoryViewModel(Doctor doctor)
        {
            InstantiateControllers();
            Medicines = new ObservableCollection<Equipement>(equipmentController.GetAllMedicine());
            loggedDoctor = doctor;
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

        public RelayCommand ReportCommand
        {
            get
            {
                return reportCommand ?? (reportCommand = new RelayCommand(param => ReportCommandExecute(), param => CanReportCommandExecute()));
            }
        }

        private bool CanReportCommandExecute()
        {
            return true;
        }

        private void ReportCommandExecute()
        {
            MedicineReportView view = new MedicineReportView();
            view.DataContext = new MedicineReportViewModel(loggedDoctor,SelectedMedicine,view);
            view.ShowDialog();
        }
    }
}
