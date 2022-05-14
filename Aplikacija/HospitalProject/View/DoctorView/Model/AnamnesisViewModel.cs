﻿using HospitalProject.Controller;
using HospitalProject.Core;
using HospitalProject.Model;
using HospitalProject.View.DoctorView.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalProject.View.DoctorView.Model
{
    public class AnamnesisViewModel : ViewModelBase
    {
        private Appointment showItem;
        private Anamnesis _anamnesis;
        private string _description;
        private AnamnesisController _anamnesisController;
        private Window _window;
        private bool modalResult;

        private RelayCommand addNewAnamnesis;
        private RelayCommand cancelNewAnamnesis;
        private RelayCommand writePrescriptionCommand;
        private RelayCommand writeReferalCommand;

        public AnamnesisViewModel(Appointment appointment, Window window)
        {
            var app = System.Windows.Application.Current as App;

            ShowItem = appointment;
            modalResult = false;
            _window = window;

            _anamnesisController = app.AnamnesisController;

        }

        public Appointment ShowItem
        {
            get
            {
                return showItem;
            }
            set
            {
                showItem = value;
                OnPropertyChanged(nameof(ShowItem));
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public bool ModalResult
        {
            get 
            { 
                return modalResult;
            }
            set
            {
                modalResult = value;
                OnPropertyChanged(nameof(ModalResult));
            }
        }

        public RelayCommand AddNewAnamnesis
        {
            get
            {
                return addNewAnamnesis ?? (addNewAnamnesis = new RelayCommand(param => AddNewAnamnesisExecute(),
                                                                               param => CanAddNewAnamnesisExecute()));
            }
        }

        private bool CanAddNewAnamnesisExecute()
        {
            return Description != null;
        }

        private void AddNewAnamnesisExecute()
        {
            _anamnesis = new Anamnesis(ShowItem, Description);
            _anamnesisController.Create(_anamnesis);
            ModalResult = true;
            _window.Close();
        }

        public RelayCommand WritePrescriptionCommand
        {
            get
            {
                return writePrescriptionCommand ?? (writePrescriptionCommand = new RelayCommand(param => WritePrescriptionCommandExecute(), param => CanWritePrescriptionCommandExecute()));
            }
        }

        
        
        private bool CanWritePrescriptionCommandExecute()
        {
            return true;
        }

        private void WritePrescriptionCommandExecute()
        {
            NewPrescriptionView view = new NewPrescriptionView();
            view.DataContext = new NewPrescriptionViewModel(view, ShowItem);
            view.ShowDialog();
        }

        public RelayCommand WriteReferalCommand
        {
            get
            {
                return writeReferalCommand ?? (writeReferalCommand = new RelayCommand(param => WriteReferalCommandExecute(), param => CanWriteReferalCommandExecute()));
            }
        }

        private void WriteReferalCommandExecute()
        {
            NewReferalView view = new NewReferalView();
            view.DataContext = new NewReferalViewModel(ShowItem.Patient, view);
            view.ShowDialog();
        }

        private bool CanWriteReferalCommandExecute()
        {
            return true;
        }
    }
}
