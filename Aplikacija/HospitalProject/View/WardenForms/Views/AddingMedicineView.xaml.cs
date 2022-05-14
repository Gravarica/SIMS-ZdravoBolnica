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
using Syncfusion.UI.Xaml.Diagram;

namespace HospitalProject.View.WardenForms.Views
{
    /// <summary>
    /// Interaction logic for AddingMedicineView.xaml
    /// </summary>
    public partial class AddingMedicineView : UserControl
    {
        
        public ObservableCollection<Equipement> MedicineItems { get; set; }
        public ObservableCollection<AddingMedicineAlergiesViewModel> AllergiesList { get; set; }

        private EquipementController _equipementController;
        private AllergiesController _allergiesController;
        public ObservableCollection<Allergies> SelectedAllergies { get; set; }

        public RelayCommand AddMedicineCommand { get; set; }

        private string selectedName;
        private int selectedQuantity;
        
       

        public AddingMedicineView(ObservableCollection<Equipement> medicineItems)
        {
            InstantiateControllers();
            InstantiateData(medicineItems);
        }

        public AddingMedicineView()
        {
        }

        private void InstantiateControllers()
        {
            var app = System.Windows.Application.Current as App;
            _equipementController = app.EquipementController;
            _allergiesController = app.AllergiesController;
        } 
        
        
        private void InstantiateData(ObservableCollection<Equipement> medicineItems)
        {
            InitializeComponent();
            DataContext = this;
            AllergiesList = new DiagramCollection<AddingMedicineAlergiesViewModel>();
            SelectedAllergies = new ObservableCollection<Allergies>();
            MedicineItems = medicineItems;
            foreach (var alergie in _allergiesController.GetAll())
            {
                AllergiesList.Add(new AddingMedicineAlergiesViewModel(alergie));
            }
            AddMedicineCommand = new RelayCommand(param => ExecuteAddingMedicineComand(), param => true);

            
        }
        private void ExecuteAddingMedicineComand()
        {
            Equipement newMedicine = new Equipement(SelectedQuantity,SelectedName,EquipementType.MEDICINE,SelectedAllergies.ToList());
            MedicineItems.Add(newMedicine);
            _equipementController.Create(newMedicine);
            EquipementViewModel wardenEquipemntViewModel =
                new EquipementViewModel();
            MainViewModel.Instance.MomentalView = wardenEquipemntViewModel;
        }
        
        
        public void ddlSupstances_SelectionChanged(object sender, SelectionChangedEventArgs e)  
        {  
  
        }  
        public void ddlSupstances_TextChanged(object sender, TextChangedEventArgs e)  
        {  
            ddlSupstances.ItemsSource = AllergiesList.Where(x => x.Name.StartsWith(ddlSupstances.Text.Trim()));
        }  
        private void AllCheckbocx_CheckedAndUnchecked(object sender, RoutedEventArgs e)  
        {  
            BindListBOX();  
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
        
        

        
    }
}
