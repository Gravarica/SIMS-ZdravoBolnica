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
    public class PrescriptionHistoryViewModel : BaseViewModel
    {

        public ObservableCollection<Prescription> Prescriptions { get; set; }
        private PrescriptionController prescriptionController;

        private Prescription selectedItem;

        public PrescriptionHistoryViewModel(MedicalRecord medicalRecord)
        {
            InstantiateControllers();
            Prescriptions = new ObservableCollection<Prescription>(prescriptionController.GetPrescriptionsForPatient(medicalRecord.Patient.Id));
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            prescriptionController = app.PrescriptionController;
        }

        public Prescription SelectedItem
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
    }
}
