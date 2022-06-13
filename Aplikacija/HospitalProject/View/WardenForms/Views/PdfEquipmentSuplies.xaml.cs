using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HospitalProject.Controller;
using HospitalProject.Model;
using Syncfusion.Data.Extensions;

namespace HospitalProject.View.WardenForms.Views;

public partial class PdfEquipmentSuplies : Window
{
    public ObservableCollection<Equipement> EquipmentItems { get; set; }
    private EquipementController _equipementController;
    public DateTime Today { get; set; }

    public PdfEquipmentSuplies()
    {
        InitializeComponent();
        DataContext = this;
        InitialiseData();
        InitialisePdfSave();
    }

    private void InitialiseData()
    {
        var app = System.Windows.Application.Current as App;
        _equipementController = app.EquipementController;
        EquipmentItems = _equipementController.GetAll().ToObservableCollection();
        Today = DateTime.Today;

    }

    private void InitialisePdfSave()
    {
        PrintDialog printDialog = new PrintDialog();
        if (printDialog.ShowDialog() == true)
        {
            printDialog.PrintVisual(print, "inovice");
            MessageBox.Show("Your are successfully created Equipment suplies report");
        }
    }
    
    
}