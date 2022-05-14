using System.Collections.ObjectModel;
using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using System;  
using System.Collections.Generic;  
using System.Windows;  
using System.Windows.Controls;  
using System.Linq;  

namespace HospitalProject.View.WardenForms.ViewModels
{
    public class AddingMedicineAlergiesViewModel : BaseViewModel
    {
        private int id;
        private String name;
        private bool isChecked;
        
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        public AddingMedicineAlergiesViewModel(int id, string name)
        {
            Name = name;
            Id = id;
            IsChecked = false;
        }
        
        public AddingMedicineAlergiesViewModel(Allergies al)
        {
            Name = al.Name;
            Id = al.Id;
            IsChecked = false;
        }
    }
}

