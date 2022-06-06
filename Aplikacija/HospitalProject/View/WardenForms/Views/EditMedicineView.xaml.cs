using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.WardenForms.ViewModels;
using Syncfusion.Linq;

namespace HospitalProject.View.WardenForms.Views

{
    /// <summary>
    /// Interaction logic for EditMedicineView.xaml
    /// </summary>
    public partial class EditMedicineView : UserControl
    {
        public ObservableCollection<Equipement> MedicineItems { get; set; }
        public ObservableCollection<AddingMedicineAlergiesViewModel> AllergiesList { get; set; }
        public ObservableCollection<EquipmentCheckBoxModel> ReplacmentList { get; set; }

        private EquipementController _equipementController;
        private AllergiesController _allergiesController;
        private MedicineReportController _medicineReportController;

        public ObservableCollection<Allergies> SelectedAllergies { get; set; }
        public ObservableCollection<Equipement> SelectedReplacements { get; set; }

        public RelayCommand EditMedicineCommand { get; set; }

        private string selectedName;
        private int selectedQuantity;
        private string doctorsComment;
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public string SelectedName
        {
            get { return selectedName; }
            set
            {
                if (value != selectedName)
                {
                    selectedName = value;
                    OnPropertyChanged(nameof(SelectedName));
                }
            }
        }
        
        public string DoctorsComment
        {
            get { return doctorsComment; }
            set
            {
                if (value != doctorsComment)
                {
                    doctorsComment = value;
                    OnPropertyChanged(nameof(DoctorsComment));
                }
            }
        }
        
        public int SelectedQuantity
        {
            get { return selectedQuantity; }
            set
            {
                if (value != selectedQuantity)
                {
                    selectedQuantity = value;
                    OnPropertyChanged(nameof(SelectedQuantity));
                }
            }
        }
        
        public EditMedicineView(MedicineReport medicineReport)
        {
            DataContext = this;
            InstantiateControllers();
            InitializeCollections();
            
            LoadAlergies();
            LoadReplacements();
            InitializeData(medicineReport);
            InitaliseCheckBocxes(medicineReport);
            InitialiseCommands(medicineReport);
            InitializeComponent();
           

        }

        private void InitialiseCommands(MedicineReport medicineReport)
        {
            EditMedicineCommand = new RelayCommand(param => ExecuteEditMedicineCommand(medicineReport), param => true);

        }

        private void ExecuteEditMedicineCommand(MedicineReport medicineReport)
        {
            Equipement editMedicine = new Equipement(medicineReport.Medicine.Id, SelectedQuantity, SelectedName,
                EquipementType.MEDICINE, SelectedAllergies.ToList(), SelectedReplacements.ToList());
            _equipementController.Update(editMedicine);
            _medicineReportController.Delete(medicineReport);
            SetMainViewToMedicineReportViewModel();
        }

        private void SetMainViewToMedicineReportViewModel()
        {
            MedicineReportViewModel mrvm = new MedicineReportViewModel();
            MainViewModel.Instance.MomentalView = mrvm;
        }
        private void LoadAlergies()
        {
            foreach (var alergie in _allergiesController.GetAll())
            {
                AllergiesList.Add(new AddingMedicineAlergiesViewModel(alergie));
            }
        }

        private void LoadReplacements()
        {
            foreach (Equipement replacement in _equipementController.GetAll())
            {
                ReplacmentList.Add(new EquipmentCheckBoxModel(replacement));
            }
        }
        
        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _equipementController = app.EquipementController;
            _allergiesController = app.AllergiesController;
            _medicineReportController = app.MedicineReportController;
        } 
        
        private void InitializeCollections()
        {
            AllergiesList = new ObservableCollection<AddingMedicineAlergiesViewModel>();
            ReplacmentList = new ObservableCollection<EquipmentCheckBoxModel>();
            SelectedAllergies = new ObservableCollection<Allergies>();
            SelectedReplacements = new ObservableCollection<Equipement>();
            
        }

        private void InitializeData(MedicineReport medicineReport)
        {
            SelectedName = medicineReport.Medicine.Name;
            SelectedQuantity = medicineReport.Medicine.Quantity;
            SelectedAllergies = medicineReport.Medicine.Alergens.ToObservableCollection();
            SelectedReplacements = medicineReport.Medicine.Replacements.ToObservableCollection();
            DoctorsComment = medicineReport.Description;
        }

        private void InitaliseCheckBocxes(MedicineReport medicineReport)
        {
            InitialiseAlergiesCheckBoxes(medicineReport);
            InitialiseReplacementsCheckBoxes(medicineReport);
            BindListBOXReplacements();

        }

        private void InitialiseAlergiesCheckBoxes(MedicineReport medicineReport)
        {
            foreach (AddingMedicineAlergiesViewModel alergie in AllergiesList)
            {
                foreach (var al in medicineReport.Medicine.Alergens)
                {
                    if (al.Id == alergie.Id)
                    {
                        alergie.IsChecked = true;
                    }
                }
            }
        }

        private void InitialiseReplacementsCheckBoxes(MedicineReport medicineReport)
        {
            foreach (EquipmentCheckBoxModel replacement in ReplacmentList)
            {
                foreach (Equipement equipment in medicineReport.Medicine.Replacements)
                {
                    if (equipment.Id == replacement.Id)
                    {
                        replacement.IsChecked = true;
                    }
                }
            }
        }
        
        public void ddlSupstances_SelectionChanged(object sender, SelectionChangedEventArgs e)  
        {  
  
        }  
        
        public void ddlReplacement_SelectionChanged(object sender, SelectionChangedEventArgs e)  
        {  
  
        }  
        public void ddlSupstances_TextChanged(object sender, TextChangedEventArgs e)  
        {  
            ddlSupstances.ItemsSource = AllergiesList.Where(x => x.Name.StartsWith(ddlSupstances.Text.Trim()));
        }  
        
        public void ddlReplacement_TextChanged(object sender, TextChangedEventArgs e)  
        {  
            ddlReplacements.ItemsSource = ReplacmentList.Where(x => x.Name.StartsWith(ddlSupstances.Text.Trim()));
        }  
        private void AllCheckbocx_CheckedAndUnchecked(object sender, RoutedEventArgs e)  
        {  
            BindListBOX();  
        }  
        
        private void AllCheckbox_Replacements_CheckedAndUnchecked(object sender, RoutedEventArgs e)  
        {  
            BindListBOXReplacements();  
        }  
        
        private void BindListBOXReplacements()  
        {  
            SelectedReplacements.Clear();  
            foreach(EquipmentCheckBoxModel replacement in ReplacmentList)  
            {
                if (replacement.IsChecked)
                {
                    SelectedReplacements.Add(new Equipement(replacement));
                }
            }  
        }  
        
        
        private void BindListBOX()  
        {  
            SelectedAllergies.Clear();  
            foreach(var alergie in AllergiesList)  
            {
                if (alergie.IsChecked)
                {
                    SelectedAllergies.Add(new Allergies(alergie));
                }
            }  
        }  
    }
}
